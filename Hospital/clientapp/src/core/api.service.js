import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import Swal from 'sweetalert2'

const ApiService = {
  init() {
    Vue.use(VueAxios, axios)
    Vue.axios.defaults.baseURL = process.env.VUE_APP_API_URL
  },

  setHeader() {},
  
  showError(data) {
    if (data.status && data.status === 401) {
      Swal.fire({
        title: 'Hata',
        text: 'Lütfen giriş yapınız.',
        icon: 'error',
        confirmButtonClass: 'btn btn-secondary',
        heightAuto: false
      }).then(() => {
        window.location.reload()
      })
      return
    } else {
      var errorText = 'Bir hata oluştu. Lütfen tekrar deneyiniz.'

      if (data.data.SummaryMessage) {
        errorText = data.data.SummaryMessage
      } else if (data.data.errors) {
        errorText = data.data.errors[Object.getOwnPropertyNames(data.data.errors)[0]][0]
      }
      Swal.fire({
        title: 'Hata',
        text: errorText,
        icon: 'error',
        confirmButtonClass: 'btn btn-secondary',
        heightAuto: false
      })
    }
  },

  query(resource, params) {
    return Vue.axios.get(resource, params).catch((error) => {
      throw new Error(`[KT] ApiService ${error}`)
    })
  },

  /**
   * Send the GET HTTP request
   * @param resource
   * @param slug
   * @returns {*}
   */
  get(resource, slug = '', params) {
    return Vue.axios.get(`${resource}/${slug}`, params).catch((error) => {
      throw new Error(`[KT] ApiService ${error}`)
    })
  },

  /**
   * Set the POST HTTP request
   * @param resource
   * @param params
   * @returns {*}
   */
  post(resource, params, slug = '') {
    return Vue.axios.post(`${resource}/${slug}`, params)
  },

  /**
   * Send the UPDATE HTTP request
   * @param resource
   * @param slug
   * @param params
   * @returns {IDBRequest<IDBValidKey> | Promise<void>}
   */
  update(resource, slug, params) {
    return Vue.axios.put(`${resource}/${slug}`, params)
  },

  /**
   * Send the PUT HTTP request
   * @param resource
   * @param params
   * @returns {IDBRequest<IDBValidKey> | Promise<void>}
   */
  put(resource, params) {
    return Vue.axios.put(`${resource}`, params)
  },

  /**
   * Send the DELETE HTTP request
   * @param resource
   * @returns {*}
   */
  delete(resource) {
    return Vue.axios.delete(resource).catch((error) => {
      throw new Error(`[RWV] ApiService ${error}`)
    })
  }
}

export default ApiService
