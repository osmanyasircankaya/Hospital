import Vue from 'vue'
import App from './App.vue'
import ApiService from '@/core/api.service'


Vue.config.productionTip = false

// API service init
ApiService.init()

new Vue({
  render: h => h(App),
}).$mount('#app')
