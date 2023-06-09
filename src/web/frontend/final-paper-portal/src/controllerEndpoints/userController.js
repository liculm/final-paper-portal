import { axiosPrivate } from '@/common/axiosPrivate'
import { axiosPublic } from '@/common/axiosPublic'

const controllerName = 'user'

export default {
  async getTest() {
    const response = await axiosPrivate.get(`${controllerName}/test`)

    return response.data
  },
  async login(data) {
    try {
      const response = await axiosPublic.post(`${controllerName}/login`, data)

      const refreshToken = response.data.refreshToken?.token
      const jwtToken = response.data.jwtToken

      if (refreshToken) {
        localStorage.getItem('refreshToken')
        localStorage.setItem('refreshToken', refreshToken)
      }

      localStorage.getItem('jwtToken')
      localStorage.setItem('jwtToken', jwtToken)

      return response
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async getAllUsers() {
    try {
      return await axiosPrivate.get(`${controllerName}/getAllUsers`)
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async getAllMentors() {
    try {
      return await axiosPrivate.get(`${controllerName}/getAllMentors`)
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async addUser (userData) {
    try {
      return await axiosPrivate.post(`${controllerName}/register`, userData)
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async deleteUser (userId) {
    try {
      return await axiosPrivate.delete(`${controllerName}/deleteUser/${userId}`)
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async updateUser (userData) {
    try {
      return await axiosPrivate.put(`${controllerName}/updateUser`, userData)
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  }
}
