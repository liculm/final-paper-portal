<template>
  <div class="home">
    <img @click="log()" alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
    as {{store.user.firstName}}
  </div>
</template>

<script>
import HelloWorld from '@/components/HelloWorld.vue'
import api from '@/services/api'
import { onMounted } from 'vue'
import { useUserStore } from '@/store/store'

export default {
  name: 'HomeView',
  setup () {

    onMounted(() => {
      // if (!this.user) {
      //   this.$router.push('login')
      // }
    })
  },
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
      const response = await api.getTest()

      console.log(response)
    }
  }
}
</script>
