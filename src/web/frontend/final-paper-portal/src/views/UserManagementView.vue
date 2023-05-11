<template>
  <div class="content">
    <p style="margin-bottom: 5em">Ova stranica namjenjena je uređivanju te pregledu svih korisnika u sustavu</p>

    <DataTable :value="userList" editMode="cell" @cell-edit-complete="onCellEditComplete($event)"
               tableClass="editable-cells-table" tableStyle="min-width: 50rem">
      <Column v-for="col of columns" :key="col.field" :field="col.field" :header="col.header" style="width: 25%">
        <template #body="{ data, field }">
          {{ data[field] }}
        </template>
        <template #editor="{ data, field }">
          <InputText v-model="data[field]" autofocus/>
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script>
import { useUserStore } from '@/store/store'
import userController from '@/controllerEndpoints/userController'

export default {
  name: 'UserManagement',
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
      userList: [],
      store: useUserStore(),
      columns: [{
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
        },
      ]
    }
  },
  methods: {
    async onCellEditComplete(event) {
      console.log(event)
      if (event.value !== event.newValue) {console.log('changed')
      }
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
