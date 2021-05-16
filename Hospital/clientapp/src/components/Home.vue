<template>
  <div>
    <h1>ÖZEL PAINT HASTANESİ</h1>
    <div>
      <b-button class="mt-5" to="/Login" variant="primary" size="lg"
        >Giriş Yap</b-button
      >
    </div>
    
  </div>
</template>

<script>
import ApiService from "@/core/api.service.js";
export default {
  name: "Home",
  data() {
    return {
      doctors: [],
      patients: [],
      polyclinics: [],
    };
  },
  methods: {
    getDoctors() {
      ApiService.setHeader();
      ApiService.get("api/Doctor")
        .then((response) => {
          this.doctors = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },
    getPatients() {
      ApiService.setHeader();
      ApiService.get("api/Patient")
        .then((response) => {
          this.patients = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },
    getPolyclinics() {
      ApiService.setHeader();
      ApiService.get("api/Polyclinic")
        .then((response) => {
          this.polyclinics = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },
  },
  mounted() {
    this.getDoctors();
    this.getPatients();
    this.getPolyclinics();
  }
};
</script>
