<template>
  <div class="login-container">
    <div class="card">
      <h1>Login</h1>
      <div class="p-fluid">
        <div class="p-field">
          <label for="username">Username: </label>
          <InputText id="username" type="text" v-model="username"/>
        </div>
        <div class="p-field">
          <label for="password">Password: </label>
          <InputText id="password" type="password" v-model="password"/>
        </div>
        <div class="p-field">
          <label for="rememberMe" class="ml-2">Remember me: </label>
          <Checkbox v-model="rememberMe" :binary="true" id="rememberMe"/>
        </div>
        <Button class="p-button p-mt-3" @click="login()">Login</Button>
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

export default {
  name: 'Login',
  data () {
    return {
      username: '',
      password: '',
      rememberMe: false,
      store: useUserStore(),
      serverResponse: null,
      validationMessages: {
        username: [
          {
            type: 'required',
            message: 'Username is required'
          }
        ],
        password: [
          {
            type: 'required',
            message: 'Password is required'
          }
        ]
      }
    }
  },
  methods: {
    async login () {
      const loginData = {
        username: this.username,
        password: this.password,
        rememberMe: this.rememberMe
      }
      const response = await userController.login(loginData)

      if (response) {
        this.store.setUser(response.data)
        this.$router.push('home')
      } else {
        this.serverResponse = 'Invalid username or password!'
      }
    }
  }
}
</script>

<style scoped>
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
