<template>
  <div class="home">
    <img @click="log()" alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
  </div>
</template>

<script>
import HelloWorld from '@/components/HelloWorld.vue'
import api from '@/services/api'

export default {
  name: 'HomeView',
  components: {
    HelloWorld
  },
  methods: {
    async log () {
      const response = await api.getTest()

      console.log(response)
    }
  }
}
</script>
