import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', {
  state: () => {
    return {
      user: null
    }
  },
  actions: {
    setUser (user) {
      this.user = user
    },
    resetUser () {
      this.user = null
    }
  },
  getters: {
    isAuthenticated () {
      return this.user !== null
    }
  },
  persist: {
    enabled: sessionStorage,
  }
})
