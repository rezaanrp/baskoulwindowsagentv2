<script setup>
defineProps({ text: String })
defineEmits(['confirm', 'cancel'])
</script>
<template><div class="dialog-backdrop" @click.self="$emit('cancel')"><div class="dialog"><h3>تأیید عملیات</h3><p>{{ text }}</p><div><button class="primary" @click="$emit('confirm')">تأیید</button><button @click="$emit('cancel')">انصراف</button></div></div></div></template>
