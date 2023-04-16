<template>
  <div class="home">
    <img @click="log()" alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
    as {{user}}
  </div>
</template>

<script>
import HelloWorld from '@/components/HelloWorld.vue'
import api from '@/services/api'
import { currentUser } from '@/services/userService'

export default {
  name: 'HomeView',
  components: {
    HelloWorld
  },
  data () {
    return {
      user: {}
    }
  },
  mounted () {
    this.user = currentUser
  },
  methods: {
    async log () {
      const response = await api.getTest()

      console.log(response)
    }
  }
}
</script>
