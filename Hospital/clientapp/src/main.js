import Vue from 'vue'
import vuetify from '@/plugins/vuetify'
import App from './App.vue'
import ApiService from '@/core/api.service'
import router from './router'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import VueMask from 'v-mask'
import VueSweetalert2 from 'vue-sweetalert2';

// Import Bootstrap an BootstrapVue CSS files (order is important)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

// Make BootstrapVue available throughout your project
Vue.use(BootstrapVue)
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin)

// If you don't need the styles, do not connect
import 'sweetalert2/dist/sweetalert2.min.css'
Vue.use(VueSweetalert2);

Vue.config.productionTip = false

// API service init
ApiService.init()

import DatetimePicker from 'vuetify-datetime-picker'
Vue.use(DatetimePicker)

Vue.use(VueMask)

new Vue({
  router,
  vuetify,
  render: h => h(App),
}).$mount('#app')
