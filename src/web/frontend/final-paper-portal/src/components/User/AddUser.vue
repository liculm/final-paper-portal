<template>
  <form @submit.prevent="addUserForm">
    <div class="grid">
      <div class="field col-6">
        <label for="username">Korisničko ime: </label>
        <InputText class="w-full" id="username" type="text" v-model="addUserForm.username"/>
        <span class="p-error" v-for="error in v$.username.$errors" :key="error.$uid">
          {{ error.$message }}
        </span>
      </div>
      <div class="field col-6">
        <label for="firstName">Ime: </label>
        <InputText class="w-full" id="firstName" type="text" v-model="addUserForm.firstName"/>
        <span class="p-error" v-for="error in v$.firstName.$errors" :key="error.$uid">
          {{ error.$message }}
        </span>
      </div>
      <div class="field col-6">
        <label for="lastName">Prezime: </label>
        <InputText class="w-full" id="lastName" type="text" v-model="addUserForm.lastName"/>
        <span class="p-error" v-for="error in v$.lastName.$errors" :key="error.$uid">
          {{ error.$message }}
        </span>
      </div>
      <div class="field col-6">
        <label for="password">Zaporka: </label>
        <InputText class="w-full" id="password" type="text" v-model="addUserForm.password"/>
        <span class="p-error" v-for="error in v$.password.$errors" :key="error.$uid">
          {{ error.$message }}
        </span>
      </div>
      <div class="field col-6">
        <label for="role">Uloga: </label>
        <Dropdown v-model="addUserForm.roleId" :options="roles()" option-value="id" optionLabel="name" placeholder="Uloga" class="w-full"/>
        <span class="p-error" v-for="error in v$.roleId.$errors" :key="error.$uid">
          {{ error.$message }}
        </span>
      </div>
      <div class="field col-12">
        <Button class="p-button p-mt-3 w-full" @click="saveUser()">Login</Button>
      </div>
    </div>
  </form>
</template>

<script>
import { defineComponent, reactive } from 'vue'
import { required } from '@vuelidate/validators'
import useVulidate from '@vuelidate/core'
import { roles } from '@/enums/roles'
import userController from '@/controllerEndpoints/userController'

export default defineComponent({
  name: 'AddUserComponent',
  setup () {
    const addUserForm = reactive({
      username: '',
      roleId: '',
      firstName: '',
      lastName: '',
      password: '',
      confirmPassword: ''
    })

    const addUserFormRules = {
      username: { required },
      roleId: { required },
      firstName: { required },
      lastName: { required },
      password: { required }
    }

    const v$ = useVulidate(addUserFormRules, addUserForm)

    return {
      addUserForm,
      v$
    }
  },
  methods: {
    roles () {
      return roles
    },
    async saveUser () {
      const validationResult = await this.v$.$validate()

      if (!validationResult) return
      this.addUserForm.confirmPassword = this.addUserForm.password

      console.log(this.addUserForm)

      const response = await userController.addUser(this.addUserForm)

      if (response) {
        console.log('asd')
      } else {
        console.log('nahhh')
      }
    }
  }
})
</script>


<style scoped>

</style>
