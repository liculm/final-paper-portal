<template>
  <div class="home">
    <img @click="log()" alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
    <div v-if="store.user"> as {{store.user.firstName}} </div>
  </div>
</template>

<script>
import HelloWorld from '@/components/HelloWorld.vue'
import { useUserStore } from '@/store/store'
import { clearUserData } from '@/services/userService'

export default {
  name: 'HomeView',
  data () {
    return {
      store: useUserStore()
    }
  },
  components: {
    HelloWorld
  },
  methods: {
    async log () {
      // This is just a test to see if the user is logged out
      clearUserData()
    }
  }
}
</script>
