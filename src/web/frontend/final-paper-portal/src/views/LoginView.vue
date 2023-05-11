<template>
  <div class="login-container">
    <div class="card">
      <h1 style="text-align: center">Login</h1>
      <div class="p-fluid">
        <form @submit.prevent="loginForm">
          <div class="p-field validation-fields">
            <label for="username">Username: </label>
            <InputText id="username" type="text" v-model="loginForm.username" />
            <span class="p-error" v-for="error in v$.username.$errors" :key="error.$uid">
              {{ error.$message }}
            </span>
          </div>
          <div class="p-field validation-fields">
            <label for="password">Password: </label>
            <InputText id="password" type="password" v-model="loginForm.password" />
            <span class="p-error" v-for="error in v$.password.$errors" :key="error.$uid">
              {{ error.$message }}
            </span>
          </div>
          <div class="p-field">
            <label for="rememberMe" class="ml-2">Remember me: </label>
            <Checkbox v-model="loginForm.rememberMe" :binary="true" id="rememberMe" />
          </div>
          <Button class="p-button p-mt-3" @click="login()">Login</Button>
        </form>
        <div v-if="serverResponse" class="error-text">
          {{ serverResponse }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useUserStore } from '@/store/store'
import userController from '@/controllerEndpoints/userController'
import useVulidate from '@vuelidate/core'
import { required } from '@vuelidate/validators'
import { reactive } from 'vue'

export default {
  name: 'Login',
  setup() {
    const loginForm = reactive({
      username: '',
      password: '',
      rememberMe: false
    })

    const loginFormRules = {
      username: { required },
      password: { required }
    }

    const v$ = useVulidate(loginFormRules, loginForm)

    return {
      loginForm,
      v$
    }
  },
  data() {
    return {
      store: useUserStore(),
      serverResponse: null
    }
  },
  methods: {
    async login() {
      this.serverResponse = null
      const validationResult = await this.v$.$validate()

      if (!validationResult) return

      const response = await userController.login(this.loginForm)

      if (response) {
        this.store.setUser(response.data)
        this.$router.push('home')
      } else {
        this.serverResponse = 'An error occured!'
      }
    }
  }
}
</script>

<style scoped>
.validation-fields {
  margin-bottom: 2em;
}

.login-container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
}

.card {
  background-color: #fff;
  padding: 2rem;
  border-radius: 0.25rem;
  box-shadow: 0 0.125rem 0.5rem rgba(0, 0, 0, 0.3);
  max-width: 20rem;
  width: 100%;
  position: static;
}

h1 {
  font-size: 2rem;
  margin-bottom: 1rem;
}

.p-field label {
  font-weight: bold;
}

.p-field input {
  width: 100%;
  border-radius: 0.25rem;
  border: 1px solid #ccc;
  padding: 0.5rem;
  margin-bottom: 1rem;
}

.p-button {
  width: 100%;
  background-color: #007be5;
  color: #fff;
  border: none;
  margin-top: 1rem;
}

.error-text {
  color: red;
  font-size: 1rem;
  margin-top: 1rem;
}
</style>
