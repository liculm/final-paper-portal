<template>
  <div class="page-content">
    <Toast />
    <Button
      label="Osvježi"
      icon="pi pi-refresh"
      class="p-button-info"
      style="margin-bottom: 30px"
      @click="hey()"
    />
    <DataTable
      :value="Students"
      paginator
      :rows="5"
      :rowsPerPageOptions="[5, 10, 20, 50]"
      tableStyle="min-width: 50rem"
    >
      <Column field="fName" header="Ime"></Column>
      <Column field="lName" header="Prezime"></Column>
      <Column field="topic" header="Tema"></Column>
      <Column field="year" header="Godina studija"></Column>
      <Column field="faculty" header="Fakultet"></Column>
      <Column header="Definiraj obranu">
        <template #body="rowData">
          <Button
            icon="pi pi-user"
            severity="info"
            aria-label="User"
            @click="showDialog(rowData)"
          />
        </template>
      </Column>
    </DataTable>

    <Dialog
      v-model="displayDialog"
      :visible="displayDialog"
      header="Definiraj obranu"
      modal
      :closable="false"
      @hide="resetDialog"
    >
      <div class="p-grid">
        <div class="input1">
          <Calendar
            class="calendar"
            v-model="input1"
            :showTime="true"
            placeholder="Odaberite datum i vrijeme"
          />
        </div>
        <div class="input">
          <Dropdown
            v-model="input2"
            :options="availableRooms"
            placeholder="Odaberite predavaonicu"
          />
        </div>
        <h4>Odaberite članove povjerenstva</h4>
        <div class="input">
          <Dropdown v-model="input3" :options="councilMembers" placeholder="1. član" />
        </div>
        <div class="input">
          <Dropdown v-model="input4" :options="councilMembers" placeholder="2. član" />
        </div>
        <div class="input">
          <Dropdown v-model="input5" :options="councilMembers" placeholder="3. član" />
        </div>
        <div class="input">
          <Button
            class="btn"
            severity="success"
            label="Definiraj obranu rada"
            @click="validateAndConfirmDialog"
          />
          <Button class="btn" severity="danger" label="Natrag" @click="cancelDialog" />
        </div>
      </div>
    </Dialog>
  </div>
</template>

<script>
export default {
  data() {
    return {
      Students: [
        {
          fName: 'Marko',
          lName: 'Marković',
          topic: 'Tematika 1',
          year: '3. godina',
          faculty: 'Fakultet A',
          availableNumberOfStudents: 3
        },
        {
          fName: 'Ana',
          lName: 'Anić',
          topic: 'Tematika 2',
          year: '2. godina',
          faculty: 'Fakultet B',
          availableNumberOfStudents: 0
        },
        {
          fName: 'Ivan',
          lName: 'Ivančić',
          topic: 'Tematika 3',
          year: '4. godina',
          faculty: 'Fakultet C',
          availableNumberOfStudents: 2
        }
      ],
      displayDialog: false,
      input1: null,
      input2: null,
      input3: [],
      input4: null,
      input5: null,
      selectedRowData: null,
      availableRooms: ['101', '329', '401', '429'],
      councilMembers: ['Member 1', 'Member 2', 'Member 3']
    }
  },
  methods: {
    hey() {
      console.log('hey')
    },
    showDialog(rowData) {
      this.selectedRowData = rowData
      this.displayDialog = true
    },
    resetDialog() {
      this.selectedRowData = null
      this.displayDialog = false
      this.input1 = null
      this.input2 = null
      this.input3 = []
      this.input4 = null
      this.input5 = null
    },
    handleCalendarConfirm() {
      this.displayDialog = false
    },
    validateAndConfirmDialog() {
      if (!this.input1 || !this.input2 || !this.input3 || !this.input4 || !this.input5) {
        this.$toast.add({
          severity: 'warn',
          summary: 'Upozorenje',
          detail: 'Molimo unesite sve podatke.',
          life: 3000
        })
        return
      }

      this.confirmDialog()
    },
    confirmDialog() {
      console.log(this.input1, this.input2, this.input3, this.input4, this.input5)

      this.$toast.add({
        severity: 'success',
        summary: 'Akcija uspješna!',
        detail:
          'Studentu je definirana obrana završnog rada dana ' +
          this.input1 +
          ' u predavaonici ' +
          this.input2 +
          '!',
        life: 5000
      })

      this.resetDialog()
    },
    cancelDialog() {
      this.resetDialog()
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

.input {
  margin-top: 10px;
  margin-bottom: 10px;
}
.input1 .calendar {
  width: 80%;
}

.btn {
  margin-top: 5px;
  margin-bottom: 5px;
  margin-right: 5px;
}
</style>
