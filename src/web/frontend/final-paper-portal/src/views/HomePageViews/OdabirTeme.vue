<template>
  <h1>Odabir Teme</h1>
  <div class="container">
    <div class="field col-6">
      <label for="topic">Unesi temu:</label>
      <InputText
        class="w-full"
        id="topic"
        type="text"
        v-model="enteredTopic"
        :class="{ 'invalid-input': !topicValidation }"
        placeholder="Unesi temu"
      />
    </div>
    <!--<button :disabled="!topicValidation" @click="selectThesisName">Pošalji temu</button>-->
    <Button
      :disabled="!topicValidation"
      @click="selectThesisName"
      label="Pošalji"
      icon="pi pi-check"
      iconPos="right"
    />
  </div>
  <Toast />
</template>

<script>
import thesisController from '@/controllerEndpoints/thesisController'
import { useUserStore } from '@/store/store'

export default {
  name: 'topicSelection',
  data() {
    return {
      enteredTopic: '',
      store: useUserStore()
    }
  },
  computed: {
    topicValidation() {
      return this.enteredTopic.trim() !== ''
    }
  },
  methods: {
    async selectThesisName() {
      try {
        const response = await thesisController.selectThesisName(
          this.store.user?.id,
          this.enteredTopic
        )
        if (response) {
          this.$toast.add({
            severity: 'success',
            summary: 'Uspješno',
            detail: 'Uspješno ste postavili temu',
            life: 3000
          })
        }
      } catch (error) {
        this.$toast.add({
          severity: 'error',
          summary: 'Greška',
          detail: 'Greška prilikom postavljanja teme',
          life: 3000
        })
        console.log(error)
      }
    }
  }
}
</script>

<style scoped>
.container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 60vh;
}

.invalid-input {
  border: 1px solid red;
}
</style>
