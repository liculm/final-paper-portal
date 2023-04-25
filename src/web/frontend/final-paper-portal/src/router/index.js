import { createRouter, createWebHistory } from 'vue-router'
import AboutView from '@/views/AboutView.vue'
import LoginView from '@/views/LoginView.vue'
import MainLayout from '@/layout/MainLayout.vue'
import HomeView from '@/views/HomeView.vue'
import UserManagementView from '@/views/UserManagementView.vue'
import { useUserStore } from '@/store/store'

const routes = [
  {
    path: '/',
    component: MainLayout,
    children: [
      {
        path: '',
        redirect: 'home'
      },
      {
        path: 'home',
        name: 'home',
        meta: {
          title: 'Home',
          requiresAuth: true
        },
        component: HomeView
      },
      {
        path: 'about',
        name: 'about',
        meta: {
          title: 'About',
          requiresAuth: true
        },
        component: AboutView
      },
      {
        path: 'userManagement',
        name: 'userManagement',
        meta: {
          title: 'Users',
          requiresAuth: true
        },
        component: UserManagementView
      }
    ]
  },
  {
    path: '/login',
    name: 'login',
    component: LoginView
  }
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes
})

router.beforeEach((to, from, next) => {
  const user = useUserStore();

  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!user.isAuthenticated()) {
      next({ name: 'login' })
    } else {
      next()
    }
  } else {
    next()
  }
})

export default router
