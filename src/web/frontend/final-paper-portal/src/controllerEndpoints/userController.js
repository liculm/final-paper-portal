import { axiosPrivate } from '@/common/axiosPrivate'
import { axiosPublic } from '@/common/axiosPublic'

const controllerName = 'user'

export default {
  async getTest () {
    const response = await axiosPrivate.get(`${controllerName}/test`);

    return response.data;
  },
  async login (data) {

    try {
      const response = await axiosPublic.post(`${controllerName}/login`, data)

      const refreshToken = response.data.refreshToken.token
      const jwtToken = response.data.jwtToken

      if (refreshToken) {
        localStorage.getItem('refreshToken')
        localStorage.setItem('refreshToken', refreshToken)
      }

      localStorage.getItem('jwtToken')
      localStorage.setItem('jwtToken', jwtToken)

      return response
    }
    catch (error) {
      console.log('Error' + error)
      return null
    }
  }
}
