<script setup>
defineProps({ model: Object, drivers: Array, lookupStatus: String, loading: Boolean })
</script>
<template>
  <section class="form-section">
    <div class="section-title"><span>۱</span><h2>پلاک و راننده</h2></div>
    <div class="field-grid">
      <label>پلاک<input ref="plate" v-model.trim="model.plate" autocomplete="off" placeholder="شماره پلاک"></label>
      <label>راننده
        <select v-model="model.driverId"><option :value="null">انتخاب راننده</option><option v-for="driver in drivers" :key="driver.id" :value="driver.id">{{ driver.title }}</option></select>
      </label>
      <label>یا نام راننده<input v-model.trim="model.driverName" placeholder="ورود دستی نام راننده"></label>
    </div>
    <p class="lookup-status" :class="{ loading }">{{ lookupStatus }}</p>
  </section>
</template>
