<script setup>
import { computed, nextTick, onBeforeUnmount, reactive, ref, watch } from "vue";
import { api } from "../api";
import LiveWeightPanel from "./LiveWeightPanel.vue";
import ManualWeightInput from "./ManualWeightInput.vue";
import PlateDriverSection from "./PlateDriverSection.vue";
import BargeDetailsSection from "./BargeDetailsSection.vue";
import ConfirmDialog from "./ConfirmDialog.vue";

const props = defineProps({
  drivers: Array,
  weighbridges: Array,
  liveWeight: Number,
  liveWeights: Object,
  liveReceivedAt: Object,
  hasLiveWeight: Boolean,
  connectionStatus: String,
  selectedScaleId: Number,
  editingBarge: Object,
});
const emit = defineEmits([
  "select-scale",
  "completed",
  "editing-cleared",
  "error",
]);
const model = reactive({
  plate: "",
  driverId: null,
  driverName: "",
  description: "",
  manual: false,
  manualWeight: 0,
});
const incomplete = ref(null);
const lookupStatus = ref("پلاک را وارد کنید.");
const lookingUp = ref(false);
const submitting = ref(false);
const showClear = ref(false);
let timer;
let controller;

const currentWeight = computed(() =>
  model.manual
    ? Number(model.manualWeight || 0)
    : Number(props.liveWeight || 0),
);
const firstWeight = computed(() => incomplete.value?.entryWeight || 0);
const displayedEntryWeight = computed(
  () => props.editingBarge?.entryWeight || firstWeight.value,
);
const displayedExitWeight = computed(() => {
  if (props.editingBarge?.exitWeight) return props.editingBarge.exitWeight;
  return incomplete.value ? currentWeight.value : 0;
});
const netWeight = computed(() =>
  props.editingBarge?.netWeight
    ? props.editingBarge.netWeight
    : firstWeight.value && currentWeight.value
      ? Math.abs(firstWeight.value - currentWeight.value)
      : 0,
);
const bargeType = computed(() =>
  props.editingBarge?.bargeType
    ? props.editingBarge.bargeType
    : !firstWeight.value
      ? "در انتظار وزن اول"
      : currentWeight.value === firstWeight.value
        ? "نامشخص"
        : currentWeight.value < firstWeight.value
          ? "ورود"
          : "خروج",
);
const isEditing = computed(
  () => !!props.editingBarge && props.editingBarge.status !== "در حال توزین",
);

watch(
  () => model.plate,
  (plate) => {
    clearTimeout(timer);
    controller?.abort();
    controller = null;
    if (props.editingBarge) return;
    incomplete.value = null;
    if (!plate) {
      lookupStatus.value = "پلاک را وارد کنید.";
      return;
    }
    timer = window.setTimeout(() => lookup(plate), 450);
  },
);

watch(
  () => props.editingBarge,
  (value) => {
    if (!value) return;
    clearTimeout(timer);
    controller?.abort();
    controller = null;
    lookingUp.value = false;
    incomplete.value = value.status === "در حال توزین" ? value : null;
    Object.assign(model, {
      plate: value.plate,
      driverId: value.driverId,
      driverName: value.driverName === "ثبت نشده" ? "" : value.driverName,
      description: value.description || "",
      manual: false,
      manualWeight: 0,
    });
    lookupStatus.value =
      value.status === "در حال توزین"
        ? `وزن اول ${Number(value.entryWeight).toLocaleString("fa-IR")} کیلوگرم بارگذاری شد؛ وزن فعلی وزن دوم است.`
        : `در حال ویرایش قبض ${value.receiptNumber || value.id}`;
  },
  { immediate: true },
);

async function lookup(plate) {
  controller = new AbortController();
  lookingUp.value = true;
  lookupStatus.value = "در حال بررسی برگه ناقص...";
  try {
    incomplete.value = await api(
      `/incomplete-by-plate?plate=${encodeURIComponent(plate)}`,
      { signal: controller.signal },
    );
    if (incomplete.value) {
      model.driverId = incomplete.value.driverId;
      model.driverName =
        incomplete.value.driverName === "ثبت نشده"
          ? ""
          : incomplete.value.driverName;
      model.description = incomplete.value.description || "";
      lookupStatus.value = `وزن اول ${Number(firstWeight.value).toLocaleString("fa-IR")} کیلوگرم پیدا شد؛ وزن فعلی وزن دوم است.`;
    } else
      lookupStatus.value =
        "برگه ناقصی وجود ندارد؛ وزن فعلی وزن اول ثبت می‌شود.";
  } catch (error) {
    if (error.name !== "AbortError") emit("error", error.message);
  } finally {
    lookingUp.value = false;
  }
}

async function submit() {
  if (submitting.value) return;
  if (
    !isEditing.value &&
    !model.manual &&
    (!props.hasLiveWeight || currentWeight.value <= 0)
  ) {
    emit("error", "باسکول فعال هنوز وزن معتبر دریافت نکرده است.");
    return;
  }
  if (
    !model.plate ||
    (!model.driverId && !model.driverName) ||
    (!isEditing.value && currentWeight.value <= 0)
  ) {
    emit(
      "error",
      isEditing.value
        ? "پلاک و راننده الزامی است."
        : "پلاک، راننده و وزن مثبت الزامی است.",
    );
    return;
  }
  submitting.value = true;
  try {
    const scaleId = props.selectedScaleId;
    const body = JSON.stringify({
      id: incomplete.value?.id || 0,
      plate: model.plate,
      weight: currentWeight.value,
      driverId: model.driverId,
      driverName: model.driverName,
      description: model.description,
      weighbridgeId: scaleId,
    });
    const result = isEditing.value
      ? await api(`/${props.editingBarge.id}`, {
          method: "PUT",
          body: JSON.stringify({
            id: props.editingBarge.id,
            plate: model.plate,
            driverId: model.driverId,
            driverName: model.driverName,
            description: model.description,
          }),
        })
      : incomplete.value
        ? await api(`/${incomplete.value.id}/second-weight`, {
            method: "PUT",
            body,
          })
        : await api("/first-weight", { method: "POST", body });
    const message = isEditing.value ? "ویرایش برگه ذخیره شد." : result.message;
    reset();
    emit("completed", message);
  } catch (error) {
    emit("error", error.message);
  } finally {
    submitting.value = false;
  }
}

function reset() {
  Object.assign(model, {
    plate: "",
    driverId: null,
    driverName: "",
    description: "",
    manual: false,
    manualWeight: 0,
  });
  incomplete.value = null;
  lookupStatus.value = "پلاک را وارد کنید.";
  emit("editing-cleared");
  nextTick(() =>
    document
      .querySelector('#baskoul-vue-app input[placeholder="شماره پلاک"]')
      ?.focus(),
  );
}

function requestClear() {
  const dirty =
    model.plate ||
    model.driverId ||
    model.driverName ||
    model.description ||
    model.manualWeight;
  if (dirty) showClear.value = true;
  else reset();
}

onBeforeUnmount(() => {
  clearTimeout(timer);
  controller?.abort();
});
</script>

<template>
  <form class="baskoul-form" @submit.prevent="submit">
    <LiveWeightPanel
      :scales="weighbridges"
      :weights="liveWeights"
      :received-at="liveReceivedAt"
      :selected-scale-id="selectedScaleId"
      :connection-status="connectionStatus"
      @select="$emit('select-scale', $event)"
    />
    <ManualWeightInput
      v-model:enabled="model.manual"
      v-model="model.manualWeight"
    />
    <PlateDriverSection
      :model="model"
      :drivers="drivers"
      :lookup-status="lookupStatus"
      :loading="lookingUp"
    />
    <BargeDetailsSection
      :model="model"
      :entry-weight="displayedEntryWeight"
      :exit-weight="displayedExitWeight"
      :net-weight="netWeight"
      :type="bargeType"
    />
    <div class="form-actions">
      <button type="submit" class="primary" :disabled="submitting">
        {{
          submitting
            ? "در حال ثبت..."
            : isEditing
              ? "ذخیره ویرایش"
              : incomplete
                ? "ثبت وزن دوم"
                : "ثبت وزن اول"
        }}</button
      ><button type="button" @click="requestClear">پاک کردن</button>
    </div>
    <ConfirmDialog
      v-if="showClear"
      text="اطلاعات واردشده پاک شود؟"
      @confirm="
        showClear = false;
        reset();
      "
      @cancel="showClear = false"
    />
  </form>
</template>
