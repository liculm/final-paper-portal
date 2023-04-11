<template>
  <div class="layout-container">
    <div class="sidebar p-shadow-4">
      <h3 class="p-m-0">Sidebar</h3>
      <ul>
        <li>
          <router-link to="/about">About</router-link>
        </li>
        <!--        <li>-->
        <!--          <router-link to="/page2">Page 2</router-link>-->
        <!--        </li>-->
      </ul>
    </div>
    <div class="content">
      <header class="p-shadow-2">
        <h1 class="p-m-0">{{ pageTitle }}</h1>
        <button class="p-button p-button-text p-button-danger p-ml-auto" @click="logout()">Logout</button>
      </header>
<!--      <main>-->
<!--        <router-view></router-view>-->
<!--      </main>-->
    </div>
  </div>
</template>

<script>

export default {
  name: 'Layout',
  data () {
    return {
      pageTitle: ''
    };
  },
  methods: {
    logout () {
      // Your logout logic goes here
    }
  },
  watch: {
    // Update the page title based on the current route
    $route (to) {
      this.pageTitle = to.meta.title;
    }
  }
};
</script>

<style scoped>
.layout-container {
  display: flex;
  height: 100vh;
}

.sidebar {
  width: 200px;
  flex-shrink: 0;
  background-color: #f5f5f5;
  padding: 1rem;
  overflow-y: auto;
}

.content {
  flex-grow: 1;
  padding: 1rem;
}

header {
  display: flex;
  align-items: center;
  padding: 1rem;
  background-color: #007be5;
  color: #fff;
}

main {
  margin-top: 1rem;
}

.p-button-danger {
  color: #ff4d4f;
}

.p-button-text {
  text-decoration: none !important;
}
</style>
