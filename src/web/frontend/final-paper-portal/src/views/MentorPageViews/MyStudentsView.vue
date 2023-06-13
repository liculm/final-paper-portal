<template>
  <div class="page-content">
    <Button
      label="Osvježi"
      icon="pi pi-refresh"
      class="p-button-info"
      style="margin-bottom: 30px"
      @click="getMentoredStudents()"
    />
    <DataTable :value="students" tableStyle="min-width: 40rem">
      <Column field="studentFirstName" header="Ime"></Column>
      <Column field="studentLastName" header="Prezime"></Column>
      <Column field="thesisName" header="Tema"></Column>
      <Column field="courseName" header="Kolegij"></Column>
      <Column header="Pošalji na završni rad">
        <template #body="rowData">
          <button
            v-if="!rowData.data.requestedThesisDefence"
            class="p-button p-button-success"
            @click="submitThesisDefenceRequest(rowData)"
          >
            <b>Pošalji na završni rad</b>
          </button>
          <p v-if="rowData.data.requestedThesisDefence">Poslano</p>
        </template>
      </Column>
    </DataTable>
  </div>
  <Toast />
</template>

<script>
import thesisController from '@/controllerEndpoints/thesisController'
import { useUserStore } from '@/store/store'

export default {
  async mounted() {
    await this.getMentoredStudents()
  },
  data() {
    return {
      students: [],
      store: useUserStore()
    }
  },
  methods: {
    async submitThesisDefenceRequest(rowData) {
      try {
        const response = await thesisController.submitThesisDefenceRequest(rowData.data.thesisId)
        if (response) {
          this.$toast.add({
            severity: 'success',
            summary: 'Uspješno',
            detail: 'Uspješno ste poslali zahtjev za završni rad',
            life: 3000
          })
          await this.getMentoredStudents()
        }
      } catch (error) {
        this.$toast.add({
          severity: 'error',
          summary: 'Greška',
          detail: 'Greška prilikom slanja zahtjeva za završni rad',
          life: 3000
        })
      }
    },
    async getMentoredStudents() {
      try {
        const response = await thesisController.getMentoredStudents(this.store.user.id)
        if (response) {
          this.students = response.data
        }
      } catch (error) {
        this.$toast.add({
          severity: 'error',
          summary: 'Greška',
          detail: 'Greška prilikom dohvaćanja studenata',
          life: 3000
        })
      }
    }
  }
}
</script>

<style scoped>
.page-content {
  height: 100%;
  margin-left: 15vw;
  margin-right: 15vw;
  margin-top: 5em;
}

.p-button {
  margin-left: 5px;
}
</style>
