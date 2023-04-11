import { createApp } from 'vue';
import App from './App.vue';
import router from './router';

import PrimeVue from 'primevue/config';
import 'primevue/resources/themes/lara-light-indigo/theme.css';
import 'primevue/resources/primevue.min.css';
import 'primeicons/primeicons.css';
import Button from 'primevue/button';
import Ripple from 'primevue/ripple';
import Sidebar from 'primevue/sidebar'
import TabMenu from 'primevue/tabmenu'
import Checkbox from 'primevue/checkbox'
import InputText from 'primevue/inputtext'
import './assets/styles.scss'

const app = createApp(App);

app.use(PrimeVue, { ripple: true });
app.use(router);

app.directive('ripple', Ripple);

app.component('Button', Button);
app.component('Sidebar', Sidebar);
app.component('TabMenu', TabMenu);
app.component('Checkbox', Checkbox);
app.component('InputText', InputText);

app.mount('#app');
