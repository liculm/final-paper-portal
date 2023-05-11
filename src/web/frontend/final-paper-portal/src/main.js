import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { createPinia } from 'pinia'
import piniaPluginPersistedState from 'pinia-plugin-persistedstate'
import PrimeVue from 'primevue/config'
import 'primevue/resources/themes/lara-light-indigo/theme.css'
import 'primevue/resources/primevue.min.css'
import 'primeicons/primeicons.css'
import Button from 'primevue/button'
import Ripple from 'primevue/ripple'
import Sidebar from 'primevue/sidebar'
import TabMenu from 'primevue/tabmenu'
import Checkbox from 'primevue/checkbox'
import InputText from 'primevue/inputtext'
import Dropdown from 'primevue/dropdown'
import RadioButton from 'primevue/radiobutton'
import Dialog from 'primevue/dialog'
import Timeline from 'primevue/timeline'
import Avatar from 'primevue/avatar'
import Calendar from 'primevue/calendar'
import './assets/styles.scss'

const app = createApp(App)
const pinia = createPinia()

pinia.use(piniaPluginPersistedState)

app.use(pinia)
app.use(PrimeVue, { ripple: true })
app.use(router)

app.directive('ripple', Ripple)

app.component('Button', Button)
app.component('Dropdown', Dropdown)
app.component('RadioButton', RadioButton)
app.component('Sidebar', Sidebar)
app.component('TabMenu', TabMenu)
app.component('Checkbox', Checkbox)
app.component('InputText', InputText)
app.component('Dialog', Dialog)
app.component('Timeline', Timeline)
app.component('Avatar', Avatar)
app.component('Calendar', Calendar)

app.mount('#app')
