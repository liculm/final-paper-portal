import { axiosPrivate } from '@/common/axiosPrivate'

const controllerName = 'thesis'

export default {
  async addThesis(courseId, studentId) {
    try {
      return await axiosPrivate.post(`${controllerName}/submitThesis`, { courseId, studentId })
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async getMentoringRequests(mentorId) {
    try {
      return await axiosPrivate.get(`${controllerName}/mentoringRequests/${mentorId}`)
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async setIsApproved(thesisId, isApproved) {
    try {
      return await axiosPrivate.put(`${controllerName}/isApproved/${thesisId}`, { isApproved })
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async selectThesisName(studentId, thesisName) {
    try {
      return await axiosPrivate.put(`${controllerName}/thesisName/${studentId}`, { thesisName })
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async getMentoredStudents(mentorId) {
    try {
      return await axiosPrivate.get(`${controllerName}/mentoredStudents/${mentorId}`)
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  },
  async submitThesisDefenceRequest(thesisId) {
    try {
      return await axiosPrivate.post(`${controllerName}/submitThesisDefenceRequest/${thesisId}`)
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  }
}
