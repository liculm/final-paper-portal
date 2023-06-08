<template>
  <div class="page-content">
    <DataTable :value="Students" tableStyle="min-width: 40rem">
      <Column field="fName" header="Ime"></Column>
      <Column field="lName" header="Prezime"></Column>
      <Column field="topic" header="Tema"></Column>
      <Column field="year" header="Godina studija"></Column>
      <Column field="faculty" header="Fakultet"></Column>
      <Column header="Prihvaćanje mentorstva">
        <template #body="rowData">
          <button
            class="p-button p-button-success"
            @click="acceptStudent(rowData)"
          >
            <b>Prihvati</b>
          </button>
          <button
            class="p-button p-button-danger"
            @click="rejectStudent(rowData)"
          >
            <b>Odbij</b>
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
        { fName: 'Marko', lName: 'Marković', topic: 'Tematika 1', year: '3. godina', faculty: 'Fakultet A', availableNumberOfStudents: 3 },
        { fName: 'Ana', lName: 'Anić', topic: 'Tematika 2', year: '2. godina', faculty: 'Fakultet B', availableNumberOfStudents: 0 },
        { fName: 'Ivan', lName: 'Ivančić', topic: 'Tematika 3', year: '4. godina', faculty: 'Fakultet C', availableNumberOfStudents: 2 },

      ],
    };
  },
  methods: {
    // eslint-disable-next-line no-unused-vars
    acceptStudent(rowData) {

    },
    // eslint-disable-next-line no-unused-vars
    rejectStudent(rowData) {

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
