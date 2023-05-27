<template>
  <div>
    <h1>Odabir Teme</h1>
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
    <button :disabled="!topicValidation" @click="sendTopic">Pošalji temu</button>
  </div>
</template>

<script>
export default {
  name: 'topicSelection',
  data() {
    return {
      enteredTopic: ''
    }
  },
  computed: {
    topicValidation() {
      return this.enteredTopic.trim() !== ''
    }
  },
  methods: {
    sendTopic() {
      console.log('Entered Topic:', this.enteredTopic)
    }
  }
}
</script>

<style scoped>
.invalid-input {
  border: 1px solid red;
}
</style>
