<template>
  <h1 class="page-title">U Obradi</h1>
  <div class="container">
    <div class="spinner-container">
      <i class="pi pi-spin pi-spinner spinner"></i>
      <span class="processing-label">Obrađuje se</span>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Obrada'
}
</script>

<style scoped>
.container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 70vh;
}

.page-title {
  font-size: 32px;
  margin-bottom: 20px;
}

.spinner-container {
  display: flex;
  align-items: center;
}

.spinner {
  font-size: 48px;
  margin-right: 10px;
}

.processing-label {
  font-size: 24px;
}
</style>
