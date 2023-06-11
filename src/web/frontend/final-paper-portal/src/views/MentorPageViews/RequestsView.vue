<template>
  <div class="page-content">
    <Button label="Osvježi" icon="pi pi-refresh" class="p-button-info" style="margin-bottom: 30px" @click="getMentoringRequests()" />
    <DataTable :value="students" tableStyle="min-width: 40rem">
      <Column field="studentFirstName" header="Ime"></Column>
      <Column field="studentLastName" header="Prezime"></Column>
      <Column field="thesisName" header="Tema"></Column>
      <Column field="courseName" header="Kolegij"></Column>
      <Column header="Prihvaćanje mentorstva">
        <template #body="rowData">
          <button
            class="p-button p-button-success"
            @click="setIsApproved(rowData, true)"
          >
            <b>Prihvati</b>
          </button>
          <button
            class="p-button p-button-danger"
            @click="setIsApproved(rowData, false)"
          >
            <b>Odbij</b>
          </button>
        </template>
      </Column>
    </DataTable>
  </div>
  <Toast />
</template>

<script>
import { useUserStore } from '@/store/store'
import thesisController from '@/controllerEndpoints/thesisController'

export default {
  async mounted() {
    await this.getMentoringRequests()
  },
  data() {
    return {
      students: [],
      store: useUserStore(),
    };
  },
  methods: {
    async setIsApproved(rowData, isApproved) {
      try {
        const response = await thesisController.setIsApproved(
          rowData.data.thesisId,
          isApproved
        );
        if (response) {
          this.$toast.add({
            severity: 'success',
            summary: 'Uspješno',
            detail: 'Uspješno ste odgovorili na zahtjev za mentorstvo',
            life: 3000,
          });
          await this.getMentoringRequests();
        }
      } catch (error) {
        console.log(error);
      }
    },
    async getMentoringRequests() {
      try {
        const response = await thesisController.getMentoringRequests(this.store.user.id)
        if (response) {
          this.students = response.data
        }
      } catch (error) {
        console.log(error)
      }
    },
  },
};
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
