import { axiosPrivate } from '@/common/axiosPrivate'

const downloadPdfEndpoint = `document/getPDFFile?fileName={fileName}`

export default {
  async openPDF(fileName) {
    try {
      const response = await axiosPrivate.post(downloadPdfEndpoint.replace("{fileName}", fileName))

      return response
    } catch (error) {
      console.log('Error' + error)
      return null
    }
  }
}
