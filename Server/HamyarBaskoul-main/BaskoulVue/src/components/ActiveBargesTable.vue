<script setup>
import { computed, ref } from "vue";
import { api } from "../api";
import { printTripleBarge } from "../print";
import StatusBadge from "./StatusBadge.vue";
import ConfirmDialog from "./ConfirmDialog.vue";

const props = defineProps({ state: Object });
const emit = defineEmits(["search", "page", "page-size", "selected", "changed", "error"]);
const search = ref("");
const pendingAction = ref(null);
const loadingRowId = ref(null);
const selectedRowId = ref(null);
const pageCount = computed(() =>
  Math.max(1, Math.ceil((props.state?.totalCount || 0) / (props.state?.pageSize || 10))),
);
const firstEntry = computed(() =>
  props.state?.totalCount ? (props.state.page - 1) * props.state.pageSize + 1 : 0,
);
const lastEntry = computed(() =>
  Math.min(props.state?.page * props.state?.pageSize || 0, props.state?.totalCount || 0),
);
const visiblePages = computed(() => {
  const current = Math.min(props.state?.page || 1, pageCount.value);
  const pages = new Set([1, pageCount.value]);
  for (let page = Math.max(1, current - 2); page <= Math.min(pageCount.value, current + 2); page++) {
    pages.add(page);
  }
  const result = [];
  for (const page of [...pages].sort((a, b) => a - b)) {
    const previous = result[result.length - 1];
    if (typeof previous === "number" && page - previous === 2) result.push(previous + 1);
    else if (typeof previous === "number" && page - previous > 2) result.push("…");
    result.push(page);
  }
  return result;
});

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
  selectedRowId.value = id;
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

function printBarge(id) {
  printTripleBarge(id);
}
</script>
<template>
  <section class="active-table-card">
    <div class="table-head">
      <div>
        <span class="eyebrow">صف عملیات</span>
        <h2><i class="fas fa-truck" aria-hidden="true"></i> ماشین‌های باسکول</h2>
      </div>
      <form @submit.prevent="$emit('search', search)">
        <input v-model="search" placeholder="پلاک یا شماره قبض" /><button>
          <i class="fas fa-search" aria-hidden="true"></i> جست‌وجو
        </button>
      </form>
    </div>
    <div class="status-guide" aria-label="راهنمای وضعیت برگه‌ها">
      <span><StatusBadge text="در حال توزین" /> وزن دوم مانده</span>
      <span><StatusBadge text="تکمیل شده" /> دو وزن ثبت شده</span>
      <span><StatusBadge text="نهایی شده" /> ثبت قطعی؛ ارسال جداست</span>
      <span><StatusBadge text="باطل شده" /> از گردش خارج</span>
      <span><StatusBadge text="نامشخص" /> وزن معتبری ندارد</span>
      <span><StatusBadge text="ارسال شده" /> ثبت در سامانهٔ مقصد</span>
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
            <th>ارسال به سامانه</th>
            <th>عملیات</th>
          </tr>
        </thead>
        <tbody>
          <tr v-if="state.loading">
            <td colspan="10" class="table-message">در حال دریافت...</td>
          </tr>
          <tr v-else-if="!state.items.length">
            <td colspan="10" class="table-message">برگه‌ای پیدا نشد.</td>
          </tr>
          <tr
            v-for="item in state.items"
            :key="item.id"
            :class="{ 'row-loading': loadingRowId === item.id, 'row-selected': selectedRowId === item.id }"
            tabindex="0"
            @click.stop="selectRow(item.id)"
            @keydown.enter.self.prevent="selectRow(item.id)"
            @keydown.space.self.prevent="selectRow(item.id)"
          >
            <td class="plate" data-label="پلاک">{{ item.plate }}</td>
            <td data-label="قبض">
              <span class="receipt-details">
                <span>{{ item.receiptNumber || "-" }}</span>
                <small v-if="item.dateBarge || item.timeBarge" class="receipt-date-time">
                  {{ [item.dateBarge, item.timeBarge].filter(Boolean).join(" ") }}
                </small>
              </span>
            </td>
            <td data-label="راننده">{{ item.driverName }}</td>
            <td data-label="ورود">
              {{ item.entryWeight?.toLocaleString("fa-IR") || "ثبت نشده" }}
            </td>
            <td data-label="خروج">
              {{ item.exitWeight?.toLocaleString("fa-IR") || "ثبت نشده" }}
            </td>
            <td data-label="خالص">{{ item.netWeight?.toLocaleString("fa-IR") || "-" }}</td>
            <td data-label="نوع"><StatusBadge :text="item.bargeType" /></td>
            <td data-label="وضعیت"><StatusBadge :text="item.status" /></td>
            <td data-label="ارسال"><StatusBadge :text="item.syncStatus" /></td>
            <td class="row-actions" data-label="عملیات">
              <span v-if="loadingRowId === item.id">در حال بارگذاری...</span
              ><template v-else
                ><button
                  type="button"
                  @click.stop.prevent="printBarge(item.id)"
                >
                  <i class="fas fa-print" aria-hidden="true"></i> چاپ</button
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
                  v-if="item.status !== 'باطل شده'"
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
    <nav class="pagination" aria-label="صفحه‌بندی برگه‌ها">
      <span class="pagination-info">
        نمایش {{ firstEntry.toLocaleString("fa-IR") }} تا
        {{ lastEntry.toLocaleString("fa-IR") }} از
        {{ state.totalCount.toLocaleString("fa-IR") }} برگه
      </span>
      <div class="pagination-pages">
        <button type="button" class="icon-only" aria-label="صفحهٔ اول" title="صفحهٔ اول"
          :disabled="state.loading || state.page <= 1" @click="$emit('page', 1)">
          <i class="fas fa-angles-right" aria-hidden="true"></i>
        </button>
        <button type="button" class="icon-only" aria-label="صفحهٔ قبلی" title="صفحهٔ قبلی"
          :disabled="state.loading || state.page <= 1" @click="$emit('page', state.page - 1)">
          <i class="fas fa-angle-right" aria-hidden="true"></i>
        </button>
        <template v-for="(page, index) in visiblePages" :key="`${page}-${index}`">
          <span v-if="page === '…'" class="pagination-ellipsis" aria-hidden="true">…</span>
          <button v-else type="button" :class="{ active: page === state.page }"
            :aria-label="`صفحه ${page}`" :aria-current="page === state.page ? 'page' : undefined"
            :disabled="state.loading" @click="page !== state.page && $emit('page', page)">
            {{ page.toLocaleString("fa-IR") }}
          </button>
        </template>
        <button type="button" class="icon-only" aria-label="صفحهٔ بعدی" title="صفحهٔ بعدی"
          :disabled="state.loading || state.page >= pageCount" @click="$emit('page', state.page + 1)">
          <i class="fas fa-angle-left" aria-hidden="true"></i>
        </button>
        <button type="button" class="icon-only" aria-label="صفحهٔ آخر" title="صفحهٔ آخر"
          :disabled="state.loading || state.page >= pageCount" @click="$emit('page', pageCount)">
          <i class="fas fa-angles-left" aria-hidden="true"></i>
        </button>
      </div>
      <label class="pagination-size" title="تعداد برگه در صفحه">
        <i class="fas fa-list" aria-hidden="true"></i>
        <select aria-label="تعداد برگه در صفحه" :value="state.pageSize" :disabled="state.loading" @change="$emit('page-size', Number($event.target.value))">
          <option :value="10">۱۰</option>
          <option :value="20">۲۰</option>
          <option :value="50">۵۰</option>
        </select>
      </label>
    </nav>
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
    </div>
    <ConfirmDialog
      v-if="pendingAction"
      :text="
        pendingAction.action === 'cancel'
          ? 'این برگه باطل شود؟ اگر قبلاً ارسال شده باشد، ابطال در همگام‌سازی بعدی منتقل می‌شود.'
          : 'این برگه نهایی شود؟'
      "
      @confirm="action"
      @cancel="pendingAction = null"
    />
  </section>
</template>
