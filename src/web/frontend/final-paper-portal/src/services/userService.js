import { useUserStore } from '@/store/store'

export function clearUserData() {
  useUserStore().resetUser()
  localStorage.clear()
  sessionStorage.clear()
}
