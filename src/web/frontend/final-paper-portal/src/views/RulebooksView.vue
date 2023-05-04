<template>
  <h1>Rulebooks Page</h1>
  <div>
    <span>Obrazac za prijavu mentora</span>
    <button @click="openPdf('obrazac-1.-prijava-mentora-i-teme-1_0_0')">Otvori</button>
  </div>
  <div>
    <span>Obrazac za prijavu obrane završnog rada </span>
    <button @click="openPdf('obrazac-2.-prijava-obrane-zavrsnog-rada-1_0_0')">Otvori</button>
  </div>
  <div>
    <span>Pravilnik o završnom / specijalističkom radu </span>
    <button @click="openPdf('pravilnik-o-zavrsnom-specijalistickom-zavrsnom-radu')">Otvori</button>
  </div>
  <div>
    <span>Odluka o obavezi korištenja software-a </span>
    <button
      @click="
        openPdf(
          '27-odluka-o-obvezi-koristenja-softvera-za-provjeru-izvornosti-zavrsnih-i-specijalistickih-zavrsnih-radova'
        )
      "
    >
      Otvori
    </button>
  </div>
  <div>
    <span>Terminski plan završnih / specijalističkih radova </span>
    <button @click="openPdf('terminski-plan-zavrsnih-specijalistickih-radova_0_0')">Otvori</button>
  </div>
</template>

<script>
const baseUrl = 'https://localhost:7169/api/v1/'
const downloadPdfEndpoint = `document/getPDFFile?fileName={fileName}`

export default {
  name: 'RulebooksView',
  methods: {
    async openPdf(fileName) {
      const response = await fetch(baseUrl + downloadPdfEndpoint.replace('{fileName}', fileName))
      const blob = await response.blob()
      const pdfUrl = URL.createObjectURL(blob)

      const pdfWindow = window.open(pdfUrl, '_blank')
      pdfWindow.focus()
    }
  }
}
</script>

<style scoped>
span:after {
  content: ' ';
  display: inline-block;
  width: 30px;
}

button {
  display: block;
  margin-bottom: 15px;
}
</style>
register
