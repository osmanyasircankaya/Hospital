import Vue from 'vue'
import vuetify from '@/plugins/vuetify'
import App from './App.vue'
import ApiService from '@/core/api.service'
import router from './router'
import { BootstrapVue, IconsPlugin } from 'bootstrap-vue'
import VueMask from 'v-mask'



// Import Bootstrap an BootstrapVue CSS files (order is important)
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

// Make BootstrapVue available throughout your project
Vue.use(BootstrapVue)
// Optionally install the BootstrapVue icon components plugin
Vue.use(IconsPlugin)

Vue.config.productionTip = false

// API service init
ApiService.init()

Vue.use(VueMask)

new Vue({
  router,
  vuetify,
  render: h => h(App),
}).$mount('#app')
