<script setup>
const props = defineProps({ text: String })
const css = { 'در حال توزین': 'pending', 'در انتظار وزن دوم': 'pending', 'تکمیل شده': 'complete', 'نهایی شده': 'final', 'باطل شده': 'cancelled', 'ورود': 'entry', 'خروج': 'exit' }
</script>
<template><span class="badge" :class="css[text] || 'unknown'">{{ text }}</span></template>
