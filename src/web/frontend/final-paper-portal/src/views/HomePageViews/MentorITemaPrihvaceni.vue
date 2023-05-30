<template>
  <h1 class="page-title">Mentor I Tema Prihvaceni</h1>
  <div class="container">
    <div class="confirmed-text">
      <i class="pi pi-check-circle green-checkmark"></i>
      <span class="confirmed-label">Tema potvrđena</span>
    </div>
    <div class="confirmed-text">
      <i class="pi pi-check-circle green-checkmark"></i>
      <span class="confirmed-label">Mentor odabran</span>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MentorAndTopicAccepted'
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
  font-size: 28px;
  margin-bottom: 20px;
}

.green-checkmark {
  color: green;
  font-size: 48px;
  margin-right: 10px;
}

.confirmed-label {
  font-size: 36px;
}

.confirmed-text {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
}
</style>
