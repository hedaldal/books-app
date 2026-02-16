import { createApp } from 'vue';
import { createVuetify } from 'vuetify';
import App from '@/App.vue';
import router from '@/router';
import 'vuetify/styles';
import '@mdi/font/css/materialdesignicons.css';

const vuetify = createVuetify();

createApp(App).use(router).use(vuetify).mount('#app');
