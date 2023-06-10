import { axiosPrivate } from '@/common/axiosPrivate'

const controllerName = 'thesis'

export default {
  async addThesis(thesisData) {
    try {
      return await axiosPrivate.post(`${controllerName}/add`, thesisData)
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  }
}
