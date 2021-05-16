<template>
  <div>
    <h1>GİRİŞ</h1>

    <div class="d-flex justify-content-center mt-5">
      <b-form @submit.stop.prevent="onSubmit">
        <b-form-group id="input-group-1" label-for="input-1">
          <b-form-input
            id="input-1"
            v-model="CitizenNumber"
            type="text"
            v-mask="'###########'"
            :state="validation"
            placeholder="Kimlik Numaranızı Giriniz"
            required
          ></b-form-input>
          <b-form-invalid-feedback :state="validation" v-if="CitizenNumber">
            Lütfen Geçerli TC Kimlik No Giriniz
          </b-form-invalid-feedback>
        </b-form-group>
        <b-button class="mt-3" type="submit" variant="primary">Giriş</b-button>
      </b-form>
    </div>
  </div>
</template>


<script>
import { mask } from "vue-the-mask";

export default {
  name: "Login",
  data() {
    return {
      CitizenNumber: "",
    };
  },
  directives: { mask },
  methods: {
    onSubmit() {
      if (this.validation) {
        alert("Form submitted!");
      } else {
        alert("Form not submitted!");
      }
    },
  },
  computed: {
    validation() {
      return this.CitizenNumber.length === 11;
    },
  },
};
</script>