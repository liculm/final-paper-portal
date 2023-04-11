import { createRouter, createWebHistory } from 'vue-router'
import AboutView from '@/views/AboutView.vue'
import LoginView from '@/views/LoginView.vue'
import App from '@/App.vue'

const routes = [
  {
    path: '/',
    component: App,
    children: [
      {
        path: '',
        redirect: 'about'
      },
      {
        path: 'about',
        name: 'about',
        meta: { title: 'About' },
        component: AboutView
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
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
