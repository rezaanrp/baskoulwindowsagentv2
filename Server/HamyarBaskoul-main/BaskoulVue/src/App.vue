<script setup>
import { computed, onBeforeUnmount, onMounted, reactive, ref } from "vue";
import * as signalR from "@microsoft/signalr";
import { api, pathBase } from "./api";
import BaskoulForm from "./components/BaskoulForm.vue";
import ActiveBargesTable from "./components/ActiveBargesTable.vue";
import ToastContainer from "./components/ToastContainer.vue";
import { printTripleBarge } from "./print";

const formData = reactive({
  drivers: [],
  weighbridges: [],
  codeMarkaz: "",
  siteId: 0,
});
const live = reactive({
  weights: {},
  receivedAt: {},
  status: "در حال اتصال",
  selectedScaleId: null,
});
const table = reactive({
  items: [],
  page: 1,
  pageSize: 10,
  totalCount: 0,
  search: "",
  loading: false,
});
const editingBarge = ref(null);
const scaleSelectionMode = ref("auto");
const toast = reactive({ text: "", type: "success" });

const selectedScale = computed(
  () =>
    formData.weighbridges.find((x) => x.id === live.selectedScaleId) ||
    formData.weighbridges[0],
);
const liveWeight = computed(() =>
  Number(live.weights[selectedScale.value?.scaleCode] || 0),
);
const hasSelectedLiveWeight = computed(
  () => !!live.receivedAt[selectedScale.value?.scaleCode],
);

function selectScale(id, announce = false, source = "manual") {
  const scale = formData.weighbridges.find((item) => item.id === Number(id));
  if (!scale) return;
  live.selectedScaleId = scale.id;
  localStorage.setItem("baskoul-vue-selected-scale", String(scale.id));
  if (source === "manual") {
    scaleSelectionMode.value = "manual";
  } else if (scaleSelectionMode.value !== "manual") {
    scaleSelectionMode.value = "auto";
  }
  if (announce) notify(`${scale.name} انتخاب شد`);
}

function selectNextScale(event) {
  if (
    event.key !== "F2" ||
    event.repeat ||
    document.querySelector(".dialog-backdrop") ||
    document.querySelector(".baskoul-form .primary:disabled")
  )
    return;
  event.preventDefault();
  const scales = formData.weighbridges;
  if (!scales.length) return;
  const currentIndex = scales.findIndex(
    (item) => item.id === live.selectedScaleId,
  );
  selectScale(scales[(currentIndex + 1) % scales.length].id, true, "manual");
}

function resetScaleSelectionMode() {
  scaleSelectionMode.value = "auto";
}

function notify(text, type = "success") {
  toast.text = text;
  toast.type = type;
  window.setTimeout(() => {
    if (toast.text === text) toast.text = "";
  }, 4500);
}

async function loadTable(page = table.page) {
  table.loading = true;
  try {
    const q = new URLSearchParams({
      page,
      pageSize: table.pageSize,
      search: table.search,
    });
    const result = await api(`/active?${q}`);
    Object.assign(table, result);
  } catch (error) {
    notify(error.message, "error");
  } finally {
    table.loading = false;
  }
}

async function openEditBarge(editId) {
  const id = Number(editId);
  if (!id) return;
  try {
    const barge = await api(`/${id}`);
    editingBarge.value = barge;
    if (barge?.weighbridgeId) {
      selectScale(barge.weighbridgeId, false, "auto");
    }
  } catch (error) {
    notify(error.message, "error");
  }
}

async function initialize() {
  try {
    Object.assign(formData, await api("/form"));
    const savedScaleId = Number(
      localStorage.getItem("baskoul-vue-selected-scale"),
    );
    const savedScaleExists = formData.weighbridges.some(
      (item) => item.id === savedScaleId,
    );
    selectScale(
      savedScaleExists ? savedScaleId : formData.weighbridges[0]?.id,
      false,
      "auto",
    );
    await loadTable(1);
    const editId = new URLSearchParams(window.location.search).get("editId");
    if (editId) {
      await openEditBarge(editId);
    }
    connectSignalR();
  } catch (error) {
    notify(error.message, "error");
  }
}

async function connectSignalR() {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl(`${pathBase}/hubs/weight`)
    .withAutomaticReconnect()
    .build();
  connection.on("ReceiveWeightUpdate", (siteId, code, scaleCode, weight) => {
    if (
      Number(siteId) === Number(formData.siteId) &&
      (!code || code === formData.codeMarkaz)
    ) {
      live.weights[scaleCode] = Number(weight) || 0;
      live.receivedAt[scaleCode] = Date.now();
    }
  });
  connection.onreconnecting(() => {
    live.status = "در حال اتصال مجدد";
  });
  connection.onreconnected(() => {
    live.status = "متصل";
  });
  connection.onclose(() => {
    live.status = "قطع";
  });
  try {
    await connection.start();
    live.status = "متصل";
  } catch {
    live.status = "قطع";
  }
}

async function completed(result) {
  const payload = typeof result === "string" ? { message: result } : result || {};
  notify(payload.message || "عملیات با موفقیت انجام شد.");
  if (payload.status === "تکمیل شده" && payload.id) {
    printTripleBarge(payload.id);
  }
  editingBarge.value = null;
  scaleSelectionMode.value = "auto";
  await loadTable(1);
}

function selectBarge(barge) {
  editingBarge.value = barge;
}

onMounted(() => {
  window.addEventListener("keydown", selectNextScale);
  initialize();
});
onBeforeUnmount(() => window.removeEventListener("keydown", selectNextScale));
</script>

<template>
  <main class="baskoul-page" dir="rtl">
    <header class="hero">
      <div>
        <span class="eyebrow">سامانه توزین</span>
        <h1>ثبت برگه باسکول</h1>
      </div>
      <span class="connection" :class="{ online: live.status === 'متصل' }">{{
        live.status
      }}</span>
    </header>
    <div class="workspace">
      <BaskoulForm
        :drivers="formData.drivers"
        :weighbridges="formData.weighbridges"
        :live-weight="liveWeight"
        :live-weights="live.weights"
        :live-received-at="live.receivedAt"
        :has-live-weight="hasSelectedLiveWeight"
        :connection-status="live.status"
        :selected-scale-id="live.selectedScaleId"
        :scale-selection-mode="scaleSelectionMode"
        :editing-barge="editingBarge"
        @select-scale="selectScale($event)"
        @auto-select-scale="selectScale($event, false, 'auto')"
        @completed="completed"
        @editing-cleared="
          editingBarge = null;
          resetScaleSelectionMode();
        "
        @error="notify($event, 'error')"
      />
      <ActiveBargesTable
        :state="table"
        @search="
          table.search = $event;
          loadTable(1);
        "
        @page="loadTable"
        @selected="selectBarge"
        @changed="loadTable()"
        @error="notify($event, 'error')"
      />
    </div>
    <ToastContainer :toast="toast" />
  </main>
</template>
