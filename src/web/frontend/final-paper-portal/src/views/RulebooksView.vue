<template>
  <div class="page-content">
    <h1>Dokumenti za Završne / Diplomske radove</h1>
    <p class="paragraph">
      Kako biste lakše i uspješnije izradili vaš rad, molimo vas da pažljivo pročitate sljedeće
      informacije i pravila te da se pridržavate svih postavljenih smjernica.
    </p>
    <div class="items">
      <div v-for="(document, index) in documents" :key="index">
        <span>{{ document.title }}</span>
        <div class="buttons">
          <Button
            class="open-pdf-button"
            label="Otvori"
            raised
            size="small"
            @click="openPdf(document.fileName)"
          />
          <Button
            class="info-button"
            icon="pi pi-info"
            outlined
            rounded
            @click="dialogs[index].visible = true"
          />
          <Dialog
            v-model:visible="dialogs[index].visible"
            modal
            :header="document.title"
            :style="{ width: '50vw' }"
          >
            <p>{{ document.description }}</p>
          </Dialog>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent, ref } from 'vue'
import documentController from '@/controllerEndpoints/documentController'

export default defineComponent({
  name: 'RulebooksView',
  setup() {
    const dialogs = ref([
      { visible: false },
      { visible: false },
      { visible: false },
      { visible: false },
      { visible: false }
    ])

    const documents = [
      {
        title: 'Obrazac za prijavu mentora',
        fileName: 'PrijavaMentora',
        description:
          'Prije nego krenete sa izradom svog rada, potrebno je da odaberete mentora koji će vam pružati stručnu pomoć i savjete. Molimo vas da ispunite obrazac za prijavu mentora i priložite ga uz vašu prijavu rada.'
      },
      {
        title: 'Obrazac za prijavu obrane završnog rada',
        fileName: 'PrijavaObrane',
        description:
          'Nakon što završite sa izradom vašeg rada, potrebno je da se prijavite za obranu. Molimo vas da ispunite obrazac za prijavu obrane završnog rada i priložite ga uz vaš završni rad.'
      },
      {
        title: 'Pravilnik o završnom / specijalističkom radu',
        fileName: 'Pravilnik',
        description:
          'Pravilnik o završnom / specijalističkom radu sadrži detaljne smjernice i pravila vezana za izradu vašeg rada. Molimo vas da pažljivo pročitate pravilnik prije nego što krenete sa izradom vašeg rada.'
      },
      {
        title: 'Odluka o obavezi korištenja software-a',
        fileName: 'SoftwareZaProvjeruIzvornosti',
        description:
          'Korištenje određenog software-a može biti obavezno prilikom izrade vašeg rada. Molimo vas da pažljivo pročitate odluku o obavezi korištenja software-a i instalirate potrebni software kako biste bili spremni za izradu vašeg rada.'
      },
      {
        title: 'Terminski plan završnih / specijalističkih radova',
        fileName: 'TerminskiPlan',
        description:
          'Terminski plan završnih / specijalističkih radova sadrži detaljan raspored vaših aktivnosti tijekom izrade rada. Molimo vas da pažljivo proučite terminski plan i pridržavate se svih postavljenih rokova kako biste uspješno završili vaš rad.'
      }
    ]

    async function openPdf(fileName) {
      const response = await documentController.openPdf(fileName)
      const blob = new Blob([response.data], { type: response.headers['content-type'] })
      const pdfUrl = window.URL.createObjectURL(blob)

      const pdfWindow = window.open(pdfUrl, '_blank')
      pdfWindow.focus()
    }

    return {
      dialogs,
      documents,
      openPdf
    }
  }
})
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
  margin-top: 50px;
  align-self: flex-start;
}

.open-pdf-button {
  margin-right: 2em;
}

.buttons {
  display: flex;
  padding: 1em;
  margin-bottom: 1em;
  height: 60px;
}

.info-button {
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
