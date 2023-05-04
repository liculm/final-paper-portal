<template>
  <div>
    <!-- User List -->
    <div class="p-card">
      <h2>User List</h2>
      <div class="p-grid">
        <div class="p-col-12">
          <div class="p-inputgroup">
            <input type="text" v-model="filter" placeholder="Search Users" class="p-inputtext" />
            <button class="p-button p-button-icon-only" @click="filter = ''">
              <i class="pi pi-times"></i>
            </button>
          </div>
        </div>
        <div class="p-col-12">
          <div class="p-datatable">
            <div class="p-datatable-header">
              <div class="p-datatable-row">
                <div class="p-datatable-col">
                  <span>Name</span>
                </div>
                <div class="p-datatable-col">
                  <span>Email</span>
                </div>
                <div class="p-datatable-col">
                  <span>Role</span>
                </div>
                <div class="p-datatable-col">
                  <span>Status</span>
                </div>
                <div class="p-datatable-col">
                  <span>Actions</span>
                </div>
              </div>
            </div>
            <div class="p-datatable-body">
              <div v-for="(user, index) in filteredUsers" :key="index" class="p-datatable-row">
                <div class="p-datatable-col">
                  <span>{{ user.name }}</span>
                </div>
                <div class="p-datatable-col">
                  <span>{{ user.email }}</span>
                </div>
                <div class="p-datatable-col">
                  <span>{{ user.role }}</span>
                </div>
                <div class="p-datatable-col">
                  <span
                    :class="{
                      'text-success': user.status === 'Active',
                      'text-danger': user.status === 'Inactive'
                    }"
                    >{{ user.status }}</span
                  >
                </div>
                <div class="p-datatable-col">
                  <button class="p-button p-button-icon-only" @click="editUser(user)">
                    <i class="pi pi-pencil"></i>
                  </button>
                  <button
                    class="p-button p-button-icon-only p-button-danger"
                    @click="deleteUser(user)"
                  >
                    <i class="pi pi-trash"></i>
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- User Form -->
    <div v-if="showUserForm" class="p-card">
      <h2>{{ isNewUser ? 'Add User' : 'Edit User' }}</h2>
      <div class="p-grid">
        <div class="p-col-12 p-md-6">
          <div class="p-field">
            <label for="name">Name</label>
            <input type="text" v-model="editedUser.name" id="name" class="p-inputtext" />
          </div>
        </div>
        <div class="p-col-12 p-md-6">
          <div class="p-field">
            <label for="email">Email</label>
            <input type="email" v-model="editedUser.email" id="email" class="p-inputtext" />
          </div>
        </div>
        <div class="p-col-12 p-md-6">
          <div class="p-field">
            <label for="password">Password</label>
          </div>
        </div>
        <div class="p-col-12 p-md-6">
          <div class="p-field">
            <label for="role">Role</label>
            <Dropdown v-model="editedUser.role" :options="roles" id="role" class="p-inputtext" />
          </div>
        </div>
        <div class="p-col-12">
          <div class="p-field">
            <label for="status">Status</label>
            <RadioButton
              v-model="editedUser.status"
              name="status"
              value="Active"
              id="active"
              class="p-inputtext"
            />
            <label for="active">Active</label>
            <RadioButton
              v-model="editedUser.status"
              name="status"
              value="Inactive"
              id="inactive"
              class="p-inputtext"
            />
            <label for="inactive">Inactive</label>
          </div>
        </div>
        <div class="p-col-12">
          <button class="p-button p-button-primary" @click="saveUser">
            {{ isNewUser ? 'Add User' : 'Save Changes' }}
          </button>
          <button class="p-button p-button-secondary" @click="cancelEdit">
            {{ isNewUser ? 'Cancel' : 'Discard Changes' }}
          </button>
        </div>
      </div>
    </div>
  </div>

  <!-- Add User Button -->
  <button v-if="!showUserForm" class="p-button p-button-primary p-mt-2" @click="addUser">
    Add User
  </button>
</template>

<script>
import { defineComponent } from 'vue'

export default defineComponent({
  data() {
    return {
      users: [
        {
          name: 'John Doe',
          email: 'johndoe@example.com',
          password: 'password',
          role: 'Admin',
          status: 'Active'
        },
        {
          name: 'Jane Smith',
          email: 'janesmith@example.com',
          password: 'password',
          role: 'User',
          status: 'Inactive'
        }
      ],
      editedUser: {
        name: '',
        email: '',
        password: '',
        role: '',
        status: 'Active'
      },
      filter: '',
      showUserForm: false,
      isNewUser: false,
      roles: ['Admin', 'User']
    }
  },
  computed: {
    filteredUsers() {
      return this.users.filter((user) => {
        const filter = this.filter.toLowerCase()
        return (
          user.name.toLowerCase().indexOf(filter) !== -1 ||
          user.email.toLowerCase().indexOf(filter) !== -1 ||
          user.role.toLowerCase().indexOf(filter) !== -1 ||
          user.status.toLowerCase().indexOf(filter) !== -1
        )
      })
    }
  },
  methods: {
    editUser(user) {
      this.editedUser = { ...user }
      this.isNewUser = false
      this.showUserForm = true
    },
    deleteUser(user) {
      const index = this.users.indexOf(user)
      this.users.splice(index, 1)
    },
    addUser() {
      this.editedUser = { name: '', email: '', password: '', role: '', status: 'Active' }
      this.isNewUser = true
      this.showUserForm = true
    },
    saveUser() {
      if (this.isNewUser) {
        this.users.push(this.editedUser)
      } else {
        const index = this.users.findIndex
        // eslint-disable-next-line no-unused-expressions
        ;(user) => user.email === this.editedUser.email

        this.$set(this.users, index, this.editedUser)
      }
      this.showUserForm = false
    },
    cancelEdit() {
      this.showUserForm = false
    }
  }
})
</script>
