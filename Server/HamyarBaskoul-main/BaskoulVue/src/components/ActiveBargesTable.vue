<script setup>
import { computed, ref } from "vue";
import { api } from "../api";
import StatusBadge from "./StatusBadge.vue";
import ConfirmDialog from "./ConfirmDialog.vue";

const props = defineProps({ state: Object });
const emit = defineEmits(["search", "page", "selected", "changed", "error"]);
const search = ref("");
const pendingAction = ref(null);
const loadingRowId = ref(null);

const pageStats = computed(() => {
  const items = props.state?.items || [];
  const activeStatuses = ["در حال توزین", "در انتظار وزن اول", "تکمیل شده"];
  return {
    total: props.state?.totalCount || 0,
    visible: items.length,
    active: items.filter((item) => activeStatuses.includes(item.status)).length,
    finalized: items.filter((item) => item.status === "نهایی شده").length,
    cancelled: items.filter((item) => item.status === "باطل شده").length,
  };
});

async function selectRow(id) {
  if (loadingRowId.value) return;
  loadingRowId.value = id;
  try {
    emit("selected", await api(`/${id}`));
  } catch (error) {
    emit("error", error.message);
  } finally {
    loadingRowId.value = null;
  }
}

async function action() {
  const current = pendingAction.value;
  pendingAction.value = null;
  try {
    await api(`/${current.id}/${current.action}`, { method: "POST" });
    emit("changed");
  } catch (error) {
    emit("error", error.message);
  }
}
</script>
<template>
  <section class="active-table-card">
    <div class="table-head">
      <div>
        <span class="eyebrow">صف عملیات</span>
        <h2>ماشین‌های باسکول</h2>
      </div>
      <form @submit.prevent="$emit('search', search)">
        <input v-model="search" placeholder="پلاک یا شماره قبض" /><button>
          جست‌وجو
        </button>
      </form>
    </div>
    <div class="table-wrap">
      <table>
        <thead>
          <tr>
            <th>پلاک</th>
            <th>قبض</th>
            <th>راننده</th>
            <th>ورود</th>
            <th>خروج</th>
            <th>خالص</th>
            <th>نوع</th>
            <th>وضعیت</th>
            <th>عملیات</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="state.loading">
            <td colspan="9">در حال دریافت...</td>
          </tr>
          <tr v-else-if="!state.items.length">
            <td colspan="9">برگه‌ای پیدا نشد.</td>
          </tr>
          <tr
            v-for="item in state.items"
            :key="item.id"
            :class="{ 'row-loading': loadingRowId === item.id }"
            @click.stop="selectRow(item.id)"
          >
            <td class="plate">{{ item.plate }}</td>
            <td>{{ item.receiptNumber || "-" }}</td>
            <td>{{ item.driverName }}</td>
            <td>
              {{ item.entryWeight?.toLocaleString("fa-IR") || "ثبت نشده" }}
            </td>
            <td>
              {{ item.exitWeight?.toLocaleString("fa-IR") || "ثبت نشده" }}
            </td>
            <td>{{ item.netWeight?.toLocaleString("fa-IR") || "-" }}</td>
            <td><StatusBadge :text="item.bargeType" /></td>
            <td><StatusBadge :text="item.status" /></td>
            <td class="row-actions">
              <span v-if="loadingRowId === item.id">در حال بارگذاری...</span
              ><template v-else
                ><button
                  v-if="!['باطل شده', 'نهایی شده'].includes(item.status)"
                  type="button"
                  @click.stop.prevent="selectRow(item.id)"
                >
                  ویرایش</button
                ><button
                  v-if="item.status === 'تکمیل شده'"
                  type="button"
                  @click.stop.prevent="
                    pendingAction = { id: item.id, action: 'finalize' }
                  "
                >
                  نهایی</button
                ><button
                  v-if="!['باطل شده', 'نهایی شده'].includes(item.status)"
                  type="button"
                  class="danger"
                  @click.stop.prevent="
                    pendingAction = { id: item.id, action: 'cancel' }
                  "
                >
                  ابطال
                </button></template
              >
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="pagination">
      <button
        type="button"
        :disabled="state.page <= 1"
        @click.stop.prevent="$emit('page', state.page - 1)"
      >
        قبلی</button
      ><span
        >صفحه {{ state.page }} از
        {{ Math.max(1, Math.ceil(state.totalCount / state.pageSize)) }}</span
      ><button
        type="button"
        :disabled="state.page * state.pageSize >= state.totalCount"
        @click.stop.prevent="$emit('page', state.page + 1)"
      >
        بعدی
      </button>
    </div>
    <div class="table-summary">
      <div class="summary-tile">
        <small>کل برگ‌ها</small>
        <strong>{{ pageStats.total.toLocaleString("fa-IR") }}</strong>
      </div>
      <div class="summary-tile">
        <small>نمایش فعلی</small>
        <strong>{{ pageStats.visible.toLocaleString("fa-IR") }}</strong>
      </div>
      <div class="summary-tile">
        <small>در جریان</small>
        <strong>{{ pageStats.active.toLocaleString("fa-IR") }}</strong>
      </div>
      <div class="summary-tile">
        <small>نهایی شده</small>
        <strong>{{ pageStats.finalized.toLocaleString("fa-IR") }}</strong>
      </div>
      <div class="summary-tile">
        <small>ابطال شده</small>
        <strong>{{ pageStats.cancelled.toLocaleString("fa-IR") }}</strong>
      </div>
      <div class="summary-note">
        <span>این بخش برای پر شدن فضای خالی زیر جدول و نمایش جمع‌بندی سریع صفحه اضافه شده است.</span>
      </div>
    </div>
    <ConfirmDialog
      v-if="pendingAction"
      :text="
        pendingAction.action === 'cancel'
          ? 'این برگه باطل شود؟'
          : 'این برگه نهایی شود؟'
      "
      @confirm="action"
      @cancel="pendingAction = null"
    />
  </section>
</template>
