<template>
  <div class="error-page">
    <h1>Error 404 - nemate dopuštenja ovoj stranici</h1>
    <h2>Preusmjeravanje uskoro...</h2>
    <div class="container">
      <div class="spinner-container">
        <i class="pi pi-spin pi-spinner spinner"></i>
        <span class="processing-label">Preusmjeravanje</span>
      </div>
    </div>
  </div>
</template>


<script>

import { ref, watchEffect, getCurrentInstance } from 'vue';

export default {
  name: 'ErrorPage',
  setup() {
    const countdown = ref(3);
    const instance = getCurrentInstance();

    watchEffect(() => {
      const timer = setInterval(() => {
        countdown.value--;
        if (countdown.value === 0) {
          clearInterval(timer);
          instance.appContext.config.globalProperties.$router.push('/rulebooks');
        }
      }, 1000);
    });

    return {
      countdown,
    };
  },
};
</script>

<style scoped>
.error-page {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

.error-page h1 {
  font-size: 24px;
  margin-bottom: 10px;
  font-family: 'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
}

.error-page h2 {
  font-size: 18px;
  margin-bottom: 20px;
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

.spinner{
  margin-right: 10px;
}
</style>
