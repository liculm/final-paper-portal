<template>
  <div class="card">
    <TabMenu :model="items" />
    <router-view />
  </div>
</template>

<script setup>

import { ref } from 'vue'

const items = ref([
  {
    label: 'Home',
    icon: 'pi pi-fw pi-home',
    to: '/home'
  },
  {
    label: 'About',
    icon: 'pi pi-fw pi-info-circle',
    to: '/about'
  },
  {
    label: 'Logout',
    icon: 'pi pi-fw pi-power-off',
    class: 'logout-button',
    to: '/login'
    // command: this.logout
  }
]);
</script>

<style>
.logout-button {
  position: fixed;
  right: 1em;
}
</style>
