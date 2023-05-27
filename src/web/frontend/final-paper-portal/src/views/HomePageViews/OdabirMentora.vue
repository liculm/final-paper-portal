<template>
  <div>
    <h1>Odabir Mentora</h1>

    <DataTable :value="Professors" tableStyle="min-width: 50rem">
      <Column field="fName" header="Ime"></Column>
      <Column field="lName" header="Prezime"></Column>
      <Column field="availableNumberOfStudents" header="Slobodnih mjesta"></Column>
      <Column field="totalNumberOfStudents" header="Ukupno mjesta"></Column>
      <Column header="Akcija">
        <template #body="rowData">
          <button
            v-if="rowData.availableNumberOfStudents > 0"
            class="p-button p-button-secondary"
            @click="selectProfessor(rowData)"
          >
            Odaberi profesora
          </button>
        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script>
export default {
  name: 'OdabirMentora',
  data() {
    return {
      Professors: [
        {
          fName: 'John',
          lName: 'Doe',
          availableNumberOfStudents: 3,
          totalNumberOfStudents: 10
        },
        {
          fName: 'Emily',
          lName: 'Smith',
          availableNumberOfStudents: 5,
          totalNumberOfStudents: 5
        },
        {
          fName: 'Michael',
          lName: 'Johnson',
          availableNumberOfStudents: 2,
          totalNumberOfStudents: 8
        },
        {
          fName: 'Jessica',
          lName: 'Williams',
          availableNumberOfStudents: 1,
          totalNumberOfStudents: 3
        }
      ]
    }
  },
  methods: {
    selectProfessor(professor) {
      // Handle the selection logic here
      console.log('Selected professor:', professor)
    }
  }
}
</script>

<style scoped>
.p-button-secondary {
  background-color: #d3d3d3;
}
</style>
