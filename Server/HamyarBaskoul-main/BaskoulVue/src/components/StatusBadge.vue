<script setup>
const props = defineProps({ text: String })
const css = { 'در حال توزین': 'pending', 'در انتظار وزن دوم': 'pending', 'تکمیل شده': 'complete', 'نهایی شده': 'final', 'باطل شده': 'cancelled', 'ورود': 'entry', 'خروج': 'exit', 'در انتظار ارسال': 'pending', 'در انتظار همگام‌سازی': 'pending', 'ابطال در انتظار ارسال': 'pending', 'ارسال شده': 'sent', 'ابطال ارسال شد': 'sent' }
</script>
<template><span class="badge" :class="css[text] || 'unknown'">{{ text }}</span></template>
