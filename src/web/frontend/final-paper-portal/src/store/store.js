import { ref } from 'vue'
import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', () => {
  const user = ref(null)

  function setUser(user) {
    this.user = user;
  }

  function isAuthenticated() {
    return !!this.user;
  }

  return { user, setUser, isAuthenticated }
})
