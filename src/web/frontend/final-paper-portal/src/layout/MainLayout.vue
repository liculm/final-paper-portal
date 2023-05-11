<template>
  <div class="card">
    <TabMenu :model="items" />
    <router-view />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { clearUserData } from '@/services/userService'

const items = ref([
  {
    label: 'Početna',
    icon: 'pi pi-fw pi-home',
    to: '/home'
  },
  {
    label: 'Poruke',
    icon: 'pi pi-fw pi-comments',
    to: '/messages'
  },
  {
    label: 'Pravilnici',
    icon: 'pi pi-fw pi-file-pdf',
    to: '/rulebooks'
  },
  {
    label: 'Pomoć',
    icon: 'pi pi-fw pi-question',
    to: '/help'
  },
  {
    label: 'Moj Profil',
    icon: 'pi pi-fw pi-user',
    class: 'my-profile',
    to: '/myProfile'
  },
  {
    label: 'Odjava',
    icon: 'pi pi-fw pi-power-off',
    class: 'logout-button',
    command: () => {
      clearUserData()
    },
    to: '/login'
  }
])
</script>

<style>
.card {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
}

.logout-button {
  position: fixed;
  right: 1em;
}

.my-profile {
  position: fixed;
  right: 9em;
}
</style>
