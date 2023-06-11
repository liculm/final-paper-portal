import { axiosPrivate } from '@/common/axiosPrivate'

const controllerName = 'thesis'

export default {
  async addThesis(courseId, studentId) {
    try {
      return await axiosPrivate.post(`${controllerName}/submitThesis`, {courseId, studentId})
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  }
}
