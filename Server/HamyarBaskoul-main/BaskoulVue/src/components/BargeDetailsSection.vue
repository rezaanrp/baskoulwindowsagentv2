<script setup>
defineProps({
  model: Object,
  entryWeight: Number,
  exitWeight: Number,
  netWeight: Number,
  type: String,
});
</script>
<template>
  <section class="form-section">
    <div class="section-title">
      <span>۲</span>
      <h2>جزئیات برگه</h2>
    </div>
    <div class="summary-grid">
      <div>
        <small>وزن ورود</small
        ><strong>{{
          entryWeight
            ? `${entryWeight.toLocaleString("fa-IR")} کیلوگرم`
            : "ثبت نشده"
        }}</strong>
      </div>
      <div>
        <small>وزن خروج</small
        ><strong>{{
          exitWeight
            ? `${exitWeight.toLocaleString("fa-IR")} کیلوگرم`
            : "ثبت نشده"
        }}</strong>
      </div>
      <div>
        <small>وزن خالص</small
        ><strong>{{
          netWeight ? netWeight.toLocaleString("fa-IR") : "ثبت نشده"
        }}</strong>
      </div>
      <div>
        <small>نوع برگه</small><strong>{{ type }}</strong>
      </div>
    </div>
    <label
      >توضیحات<textarea
        v-model.trim="model.description"
        rows="2"
        placeholder="توضیحات اختیاری"
      ></textarea>
    </label>
  </section>
</template>
