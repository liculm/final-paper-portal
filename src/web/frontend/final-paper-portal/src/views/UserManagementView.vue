<template>
  <div class="content">
    <p style="margin-bottom: 5em">
      Ova stranica namjenjena je uređivanju te pregledu svih korisnika u sustavu
    </p>

    <Button
      label="Dodaj korisnika"
      icon="pi pi-plus"
      class="p-button-success"
      @click="addUser"
    />

    <Dialog
      v-model:visible="addDialogOpen"
      :style="{ width: '50vw' }"
      :draggable="false"
      header="Dodaj korisnika"
      modal
    >
      <AddUserComponent></AddUserComponent>
      <template #footer>
        <Button label="No" icon="pi pi-times" @click="addDialogOpen = false" text />
        <Button label="Yes" icon="pi pi-check" @click="addDialogOpen = false" autofocus />
      </template>
    </Dialog>

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
          {{ data[field] }}
        </template>
        <template #editor="{ data, field }">
          <InputText v-model="data[field]"/>
        </template>
      </Column>
      <Column :rowEditor="true" style="width: 10%; min-width: 8rem" bodyStyle="text-align:center"></Column>
    </DataTable>
  </div>
</template>

<script>
import { useUserStore } from '@/store/store'
import userController from '@/controllerEndpoints/userController'
import AddUserComponent from '@/components/User/AddUser.vue'

export default {
  name: 'UserManagement',
  components: { AddUserComponent },
  async mounted () {
    try {
      const response = await userController.getAllUsers()
      if (response) {
        this.userList = response.data
      }
    } catch (error) {
      console.log(error)
    }
  },
  data () {
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
          field: 'roleName',
          header: 'Uloga'
        }
      ]
    }
  },
  methods: {
    async onRowEditSave (event) {
      console.log(event)
      if (event.data !== event.newData) {
        console.log('changed')
      }
    },
    addUser () {
      this.addDialogOpen = true
    }
  }
}
</script>

<style>
.content {
  margin: 0 auto;
  width: 80%;
  padding: 2rem;
}
</style>
