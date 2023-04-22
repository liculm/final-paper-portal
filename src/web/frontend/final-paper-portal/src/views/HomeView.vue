<template>
  <div class="home">
    <img @click="log()" alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
    as
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

    const store = useUserStore();

    onMounted(() => {
      const user = store.user;

      console.log(user);
      // if (!this.user) {
      //   this.$router.push('login')
      // }
    })
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
