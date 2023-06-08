<template>
  <div class="page-content">
    <DataTable :value="Students" tableStyle="min-width: 40rem">
      <Column field="fName" header="Ime"></Column>
      <Column field="lName" header="Prezime"></Column>
      <Column field="year" header="Godina studija"></Column>
      <Column field="topic" header="Tema"></Column>
      <Column header="Pošalji na završni rad">
        <template #body="rowData">
          <button
            class="p-button p-button-success"
            @click="sendStudent (rowData)"
          >
            <b>Pošalji na završni rad</b>
          </button>



        </template>
      </Column>
    </DataTable>
  </div>
</template>

<script>
export default {
  data() {
    return {
      Students: [
        { fName: 'Marko', lName: 'Marković', year: '1', topic: 'Tematika 1', availableNumberOfStudents: 3 },
        { fName: 'Ana', lName: 'Anić', year: '2', topic: 'Tematika 2', availableNumberOfStudents: 0 },
        { fName: 'Ivan', lName: 'Ivančić', year: '2', topic: 'Tematika 3', availableNumberOfStudents: 2 },
      ],
    };
  },
  methods: {
    // eslint-disable-next-line no-unused-vars
    sendStudent(rowData) {

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
