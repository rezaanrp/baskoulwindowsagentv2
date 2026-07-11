<script setup>defineProps({ toast: Object })</script>
<template><Transition name="toast"><div v-if="toast.text" class="toast-message" :class="toast.type">{{ toast.text }}</div></Transition></template>
