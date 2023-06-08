<template>
  <div class="card">
    <TabMenu :model="items" />
    <router-view />
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { clearUserData } from '@/services/userService'
import { useUserStore } from '@/store/store'

const store = useUserStore();

const role = store.user?.roleName

const items = ref([
  {
    label: 'Početna',
    icon: 'pi pi-fw pi-home',
    to: '/home'
  },
  {
    label: 'Poruke',
    icon: 'pi pi-fw pi-comments',
    to: '/messages',
    class: role !== 'Admin' ? '' : 'hide'
  },
  {
    label: 'Pravilnici',
    icon: 'pi pi-fw pi-file-pdf',
    to: '/rulebooks'
  },
  {
    label: 'Pomoć',
    icon: 'pi pi-fw pi-question',
    to: '/help',

  },
  {
    label: 'Korisnici',
    icon: 'pi pi-fw pi-user',
    to: '/users',
    class: role === 'Admin' ? '' : 'hide'
  },
  {
    label: 'Zahtjevi za mentorstvo',
    icon: 'pi pi-fw pi-user-plus',
    to: '/requests',
    class: role === 'Mentor' ? '' : 'hide'


  },
  {
    label: 'Studenti pod Mentorstvom',
    icon: 'pi pi-fw pi-user',
    to: '/myStudents',
    class: role === 'Mentor' ? '' : 'hide'
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

.hide{
  display: none;
}
</style>
