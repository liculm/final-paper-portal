<template>
  <div class="login-container">
    <div class="card">
      <h1>Login</h1>
      <div class="p-fluid">
        <div class="p-field">
          <label for="username">Username</label>
          <input id="username" type="text" v-model="username" />
        </div>
        <div class="p-field">
          <label for="password">Password</label>
          <input id="password" type="password" v-model="password" />
        </div>
        <button class="p-button p-mt-3" @click="login()">Login</button>
      </div>
    </div>
  </div>
</template>

<script>
import api from '@/services/api'

export default {
  name: 'Login',
  data () {
    return {
      username: '',
      password: '',
      resource: null
    };
  },
  methods: {
    async login () {
      const loginData = {
        username: this.username,
        password: this.password,
        rememberMe: true
      }

      const resource = await api.login(loginData);
      this.resource = resource;
    }
  }
};
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
</style>
