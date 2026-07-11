<script setup>
defineProps({ enabled: Boolean, modelValue: Number })
defineEmits(['update:enabled', 'update:modelValue'])
</script>
<template>
  <div class="manual-weight">
    <label class="switch"><input type="checkbox" :checked="enabled" @change="$emit('update:enabled', $event.target.checked)"> ورود دستی وزن</label>
    <input v-if="enabled" type="number" min="1" step="1" :value="modelValue || ''" placeholder="وزن به کیلوگرم"
      @input="$emit('update:modelValue', Number($event.target.value))">
  </div>
</template>
