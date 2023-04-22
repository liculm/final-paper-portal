import { useUserStore } from '@/store/store'

export function isAuthenticated() {
  const user = useUserStore();

  return user.isAuthenticated()
}
