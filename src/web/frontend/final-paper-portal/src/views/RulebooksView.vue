<template>
  <div class="page-content">
    <h1>Dokumenti za Završne / Diplomske radove</h1>
    <p class="paragraph">Kako biste lakše i uspješnije izradili vaš rad, molimo vas da pažljivo pročitate sljedeće
      informacije i pravila te da se pridržavate svih postavljenih smjernica.</p>
    <div class="items">
      <div>
        <span>
        <Button class="info-button" icon="pi pi-info" outlined rounded
                @click="dialogs[0].visible = true"/>
        Obrazac za prijavu mentora
        </span>
        <div class="buttons">
          <Button class="open-pdf-button" label="Otvori" raised
                  @click="openPdf('obrazac-1.-prijava-mentora-i-teme-1_0_0')"/>
          <Dialog v-model:visible="dialogs[0].visible" modal header="Obrazac za prijavu mentora"
                  :style="{ width: '50vw' }">
            <p>
              Prije nego krenete sa izradom svog rada, potrebno je da odaberete mentora koji će vam pružati stručnu
              pomoć i savjete. Molimo vas da ispunite obrazac za prijavu mentora i priložite ga uz vašu prijavu rada.
            </p>
          </Dialog>
        </div>
      </div>
      <div>
        <span>Obrazac za prijavu obrane završnog rada</span>
        <div class="buttons">
          <Button class="open-pdf-button" label="Otvori" raised size="small"
                  @click="openPdf('obrazac-2.-prijava-obrane-zavrsnog-rada-1_0_0')"/>
          <Button icon="pi pi-info" outlined rounded iconClass="info-button"
                  @click="dialogs[1].visible = true"/>
          <Dialog v-model:visible="dialogs[1].visible" modal header="Obrazac za prijavu obrane završnog rada"
                  :style="{ width: '50vw' }">
            <p>
              Nakon što završite sa izradom vašeg rada, potrebno je da se prijavite za obranu. Molimo vas da ispunite
              obrazac za prijavu obrane završnog rada i priložite ga uz vaš završni rad.
            </p>
          </Dialog>
        </div>
      </div>
      <div>
        <span>Pravilnik o završnom / specijalističkom radu</span>
        <div class="buttons">
          <Button class="open-pdf-button" label="Otvori" size="small"
                  @click="openPdf('pravilnik-o-zavrsnom-specijalistickom-zavrsnom-radu')"/>
          <Button icon="pi pi-info" outlined rounded size="small"
                  @click="dialogs[2].visible = true"/>
          <Dialog v-model:visible="dialogs[2].visible" modal header="Pravilnik o završnom / specijalističkom radu"
                  :style="{ width: '50vw' }">
            <p>
              Pravilnik o završnom / specijalističkom radu sadrži detaljne smjernice i pravila vezana za izradu vašeg
              rada. Molimo vas da pažljivopročitate pravilnik prije nego što krenete sa izradom vašeg rada.
            </p>
          </Dialog>
        </div>
      </div>
      <div>
        <span>Odluka o obavezi korištenja software-a</span>
        <div class="buttons">
          <Button class="open-pdf-button" label="Otvori" size="small"
                  @click="openPdf('27-odluka-o-obvezi-koristenja-softvera-za-provjeru-izvornosti-zavrsnih-i-specijalistickih-zavrsnih-radova')"/>
          <Button icon="pi pi-info" outlined rounded size="small"
                  @click="dialogs[3].visible = true"/>
          <Dialog v-model:visible="dialogs[3].visible" modal header="Odluka o obavezi korištenja software-a"
                  :style="{ width: '50vw' }">
            <p>
              Korištenje određenog software-a može biti obavezno prilikom izrade vašeg rada. Molimo vas da pažljivo
              pročitate odluku o obavezi korištenja software-a i instalirate potrebni software kako biste bili spremni
              za izradu vašeg rada.
            </p>
          </Dialog>
        </div>
      </div>
      <div>
        <span>Terminski plan završnih / specijalističkih radova</span>
        <div class="buttons">
          <Button class="open-pdf-button" label="Otvori" size="small"
                  @click="openPdf('terminski-plan-zavrsnih-specijalistickih-radova_0_0')"/>
          <Button icon="pi pi-info" outlined rounded size="small"
                  @click="dialogs[4].visible = true"/>
          <Dialog v-model:visible="dialogs[4].visible" modal header="Terminski plan završnih / specijalističkih radova"
                  :style="{ width: '50vw' }">
            <p>
              Terminski plan završnih / specijalističkih radova sadrži detaljan raspored vaših aktivnosti tijekom izrade
              rada. Molimo vas da pažljivo proučite terminski plan i pridržavate se svih postavljenih rokova kako biste
              uspješno završili vaš rad.
            </p>
          </Dialog>
        </div>
      </div>

    </div>
  </div>
  <div>
    <span>Obrazac za prijavu obrane završnog rada </span>
    <button @click="openPdf('obrazac-2.-prijava-obrane-zavrsnog-rada-1_0_0')"> Otvori </button>
  </div>
  <div>
    <span>Pravilnik o završnom / specijalističkom radu </span>
    <button @click="openPdf('pravilnik-o-zavrsnom-specijalistickom-zavrsnom-radu')"> Otvori </button>
  </div>
  <div>
    <span>Odluka o obavezi korištenja software-a </span>
    <button @click="openPdf('27-odluka-o-obvezi-koristenja-softvera-za-provjeru-izvornosti-zavrsnih-i-specijalistickih-zavrsnih-radova')"> Otvori </button>
  </div>
  <div>
    <span>Terminski plan završnih / specijalističkih radova </span>
    <button @click="openPdf('terminski-plan-zavrsnih-specijalistickih-radova_0_0')"> Otvori </button>
  </div>
</template>
<script>
import { defineComponent, ref } from 'vue'

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

.paragraph {
  font-size: larger;
  font-stretch: semi-condensed;
  padding-bottom: 40px;
}

.items {
  align-self: flex-start;
}

.open-pdf-button {
  margin-right: 2em;
}

.buttons {
  display: flex;
  padding: 1em;
  margin-bottom: 1.5em;
}

.info-button {
  font-size: 0.8em;
  height: 1.5em !important;
  width: 1.5em !important;
}

.page-content {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
  margin-left: 25vw;
  margin-right: 25vw;
}
</style>
