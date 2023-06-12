import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/LoginView.vue'
import MainLayout from '@/layout/MainLayout.vue'
import HomeView from '@/views/HomeView.vue'
import UserManagementView from '@/views/UserManagementView.vue'
import { useUserStore } from '@/store/store'
import MessagesView from '@/views/MessagesView.vue'
import RulebooksView from '@/views/RulebooksView.vue'
import HelpView from '@/views/HelpView.vue'
import MyStudentsView from '@/views/MentorPageViews/MyStudentsView.vue'
import RequestsView from '@/views/MentorPageViews/RequestsView.vue'
import DefenceList from '@/views/CouncilPageViews/DefenceList.vue'

const routes = [
  {
    path: '/',
    component: MainLayout,
    children: [
      {
        path: '',
        redirect: 'rulebooks'
      },
      {
        path: 'home',
        name: 'home',
        meta: {
          title: 'Home',
          requiresAuth: true,
          allowedRoles: ['Student']
        },
        component: HomeView
      },
      {
        path: 'messages',
        name: 'messages',
        meta: {
          title: 'messages',
          requiresAuth: true
        },
        component: MessagesView
      },
      {
        path: 'rulebooks',
        name: 'rulebooks',
        meta: {
          title: 'rulebooks',
          requiresAuth: true
        },
        component: RulebooksView
      },
      {
        path: 'help',
        name: 'help',
        meta: {
          title: 'help',
          requiresAuth: true
        },
        component: HelpView
      },
      {
        path: 'users',
        name: 'users',
        meta: {
          title: 'users',
          requiresAuth: true,
          allowedRoles: ['Admin']
        },
        component: UserManagementView
      },
      {
        path: '/myStudents',
        name: 'myStudents',
        meta: {
          title: 'myStudents',
          requiresAuth: true,
          allowedRoles: ['Mentor']
        },
        component: MyStudentsView
      },
      {
        path: '/requests',
        name: 'requests',
        meta: {
          title: 'requests',
          requiresAuth: true,
          allowedRoles: ['Mentor']
        },
        component: RequestsView
      },
      {
        path: '/studentsForDefense',
        name: 'studentsForDefense',
        meta: {
          title: 'studentsForDefense',
          requiresAuth: true,
          allowedRoles: ['Admin']
        },
        component: DefenceList
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
  const allowedRoles = to.meta.allowedRoles

  if (allowedRoles && allowedRoles.length > 0) {
    const userRole = useUserStore().user.roleName

    if (!allowedRoles.includes(userRole)) {
      next({ name: from.name })
    }
  }

  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (to.name === 'login' && useUserStore().isAuthenticated) {
      this.clearUserData()
    }

    if (!useUserStore().isAuthenticated) {
      next({ name: 'login' })
    } else {
      next()
    }
  } else {
    next()
  }
})

export default router
