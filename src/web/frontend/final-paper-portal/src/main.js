import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import Button from 'primevue/button'

createApp(App).use(router).mount('#app')

const app = createApp(App);

app.use(Button);
