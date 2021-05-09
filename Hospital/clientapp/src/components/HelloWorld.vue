<template>
  <div>
    <h1>HASTANE</h1>
    <div>
      <h4>Doktorlar:</h4>
      <div v-for="item in doctors" :key="item.Id">
        <span>{{ item.name }}</span>
      </div>
      <h4>Hastalar:</h4>
      <div v-for="item in patients" :key="item.Id">
        <span>{{ item.citizenNumber }}</span>
      </div>
      <h4>Poliklinikler:</h4>
      <div v-for="item in polyclinics" :key="item.Id">
        <span>{{ item.name }}</span>
      </div>
    </div>
  </div>
</template>

<script>
import ApiService from "@/core/api.service.js";
export default {
  name: "HelloWorld",
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
  },
  props: {
    msg: String,
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
