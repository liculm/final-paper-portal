<template>
  <div>
    <h1>Odabir Mentora</h1>

    <DataTable :value="mentorList" data-key="id" tableStyle="min-width: 50rem">
      <Column field="firstName" header="Ime"></Column>
      <Column field="lastName" header="Prezime"></Column>
      <Column field="courseName" header="Kolegij"></Column>
      <Column field="availableNumberOfStudents" header="Slobodnih mjesta"></Column>
      <Column field="totalNumberOfStudents" header="Ukupno mjesta"></Column>
      <Column header="Akcija">
        <template #body="rowData">
          <button
            class="p-button p-button-secondary"
            @click="onMentorSelect(rowData.data.courseId)"
          >
            Odaberi mentora
          </button>
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script>
import userController from '@/controllerEndpoints/userController'
import thesisController from '@/controllerEndpoints/thesisController'
import { useUserStore } from '@/store/store'

export default {
  name: 'OdabirMentora',
  async mounted() {
    await this.getAllMentors()
  },
  data() {
    return {
      mentorList: [],
      totalNumberOfStudents: 10,
      store: useUserStore()
    }
  },
  methods: {
    async getAllMentors() {
      try {
        const response = await userController.getAllMentors()
        if (response) {
          this.mentorList = response.data
          console.log('Response from getAllMentors:', this.mentorList)
        }
      } catch (error) {
        console.log(error)
      }
    },
    async onMentorSelect(courseId) {
      const response = await thesisController.addThesis(courseId, this.store.user?.id)

      if (response) {
        this.$toast.add({
          severity: 'success',
          summary: 'Uspješno',
          detail: 'Mentoru je poslan zahtjev za mentorstvo!',
          life: 3000
        })
      } else {
        this.$toast.add({
          severity: 'error',
          summary: 'Greška',
          detail: 'Došlo je do greške, pokušajte kasnije!',
          life: 3000
        })
      }
    }
  }
}
</script>

<style scoped>
.p-button-secondary {
  background-color: #d3d3d3;
}
</style>
