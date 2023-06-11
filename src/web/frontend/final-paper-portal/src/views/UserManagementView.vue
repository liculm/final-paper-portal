<template>
  <div class="content">
    <h1 class="title">Dodavanje / Udređivanje korisnika</h1>

    <div class="buttons">
      <Button
        label="Dodaj korisnika"
        icon="pi pi-plus"
        class="p-button-success"
        style="margin-right: 1em"
        @click="addUser"
      />
      <Button label="Osvježi" icon="pi pi-refresh" class="p-button-info" @click="getAllUsers" />
    </div>

    <Dialog
      v-model:visible="addDialogOpen"
      :style="{ width: '50vw' }"
      :draggable="false"
      header="Dodaj korisnika"
      modal
    >
      <AddUserComponent @toggleDialog="toggleDialog"></AddUserComponent>
    </Dialog>

    <div class="table-container">
      <DataTable
        v-model:editingRows="editingRows"
        :value="userList"
        editMode="row"
        dataKey="id"
        @row-edit-save="onRowEditSave($event)"
        tableClass="editable-cells-table"
        tableStyle="min-width: 50rem"
        selectionMode="single"
      >
        <Column
          v-for="col of columns"
          :key="col.field"
          :field="col.field"
          :header="col.header"
          style="width: 25%"
        >
          <template #body="{ data, field }">
            <div v-if="field === 'roleId'">
              {{ getRole(data.roleId).name }}
            </div>
            <div v-else>
              {{ data[field] }}
            </div>
          </template>
          <template #editor="{ data, field }">
            <div v-if="field === 'roleId'">
              <Dropdown
                v-model="data[field]"
                :options="roles()"
                optionLabel="name"
                optionValue="id"
                class="w-full"
              />
            </div>
            <div v-else>
              <InputText v-model="data[field]" />
            </div>
          </template>
        </Column>
        <Column
          :rowEditor="true"
          style="width: 10%; min-width: 8rem"
          bodyStyle="text-align:center"
        ></Column>
      </DataTable>
    </div>
  </div>
  <Toast />
</template>

<script>
import { useUserStore } from '@/store/store'
import userController from '@/controllerEndpoints/userController'
import AddUserComponent from '@/components/User/AddUser.vue'
import { roles } from '@/enums/roles'

export default {
  name: 'UserManagement',
  components: { AddUserComponent },
  async mounted() {
    await this.getAllUsers()
  },
  data() {
    return {
      addDialogOpen: false,
      editingRows: [],
      userList: [],
      store: useUserStore(),
      columns: [
        {
          field: 'username',
          header: 'Korisničko ime'
        },
        {
          field: 'firstName',
          header: 'Ime'
        },
        {
          field: 'lastName',
          header: 'Prezime'
        },
        {
          field: 'roleId',
          header: 'Uloga'
        }
      ]
    }
  },
  methods: {
    toggleDialog() {
      this.addDialogOpen = !this.addDialogOpen
    },
    getRole(roleId) {
      const allRoles = this.roles()
      return allRoles.find((role) => role.id === roleId)
    },
    roles() {
      return roles
    },
    async onRowEditSave(event) {
      if (event.data === event.newData) return

      try {
        const response = await userController.updateUser(event.newData)
        if (response) {
          this.userList = this.userList.map((user) => {
            if (user.id === event.data.id) {
              return event.newData
            }
            return user
          })

          this.$toast.add({
            severity: 'success',
            summary: 'Uspješno',
            detail: 'Korisnik je uspješno ažuriran',
            life: 3000
          })
        }
      } catch (error) {
        this.$toast.add({
          severity: 'error',
          summary: 'Greška',
          detail: 'Korisnik nije ažuriran',
          life: 3000
        })
        console.log(error)
      }
    },
    async getAllUsers() {
      try {
        const response = await userController.getAllUsers()
        if (response) {
          this.userList = response.data
        }
      } catch (error) {
        console.log(error)
      }
    },
    addUser() {
      this.addDialogOpen = true
    }
  }
}
</script>

<style>
.title {
  margin-bottom: 80px;
}

.content {
  margin: 0 auto;
  width: 80%;
  padding: 2rem;
}

.buttons {
  margin-bottom: 30px;
}

.table-container {
  height: 400px;
  overflow-y: auto; 
}
</style>
