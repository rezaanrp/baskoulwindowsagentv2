<script setup>
defineProps({
  scales: { type: Array, default: () => [] },
  weights: { type: Object, default: () => ({}) },
  receivedAt: { type: Object, default: () => ({}) },
  selectedScaleId: Number,
  connectionStatus: String,
});
defineEmits(["select"]);
const formatTime = (value) =>
  value ? new Date(value).toLocaleTimeString("fa-IR") : "";
</script>

<template>
  <section class="live-weights-panel">
    <div class="live-weights-heading">
      <div>
        <strong>وزن زنده باسکول‌ها</strong><small>{{ connectionStatus }}</small>
      </div>
      <kbd>تغییر باسکول: F2</kbd>
    </div>
    <div v-if="scales.length" class="weight-cards">
      <button
        v-for="(scale, index) in scales"
        :key="scale.id"
        type="button"
        class="weight-card"
        :class="{ selected: selectedScaleId === scale.id }"
        :aria-pressed="selectedScaleId === scale.id"
        @click="$emit('select', scale.id)"
      >
        <span class="scale-index">{{ index + 1 }}</span>
        <span class="scale-name">{{ scale.name }}</span>
        <span class="scale-code">کد {{ scale.scaleCode }}</span>
        <strong v-if="receivedAt[scale.scaleCode]"
          >{{ Number(weights[scale.scaleCode]).toLocaleString("fa-IR") }}
          <small>کیلوگرم</small></strong
        >
        <strong v-else class="waiting">در انتظار دریافت وزن</strong>
        <small v-if="receivedAt[scale.scaleCode]" class="last-update"
          >آخرین دریافت: {{ formatTime(receivedAt[scale.scaleCode]) }}</small
        >
        <span v-if="selectedScaleId === scale.id" class="selected-label"
          >فعال</span
        >
      </button>
    </div>
    <div v-else class="no-scales">باسکولی برای سایت جاری تعریف نشده است.</div>
  </section>
</template>
