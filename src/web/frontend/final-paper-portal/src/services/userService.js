function getUser () {
  const userString = localStorage.getItem('user')

  if (!userString)
    return null

  const data = JSON.parse(userString)

  return data.user
}

export const currentUser = getUser()


