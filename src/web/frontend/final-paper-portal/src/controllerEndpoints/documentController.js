import { axiosPrivate } from '@/common/axiosPrivate'

const downloadPdfEndpoint = `document/getPDFFile`

export default {
  async openPdf(fileName) {
    try {
      const data = {
        fileName: fileName
      }

      return await axiosPrivate.post(downloadPdfEndpoint, data, { responseType: 'blob' })
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  }
}
