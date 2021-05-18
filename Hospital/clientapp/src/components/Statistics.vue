<template>
  <div>
    <h1>İSTATİSTİKLER</h1>
    <div class="d-flex justify-content-center mt-5 mb-5">
      <v-simple-table>
        <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">Kimlik Numarası</th>
              <th class="text-left">Kayıt Tarihi</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in patients" :key="item.citizenNumber">
              <td>{{ item.citizenNumber }}</td>
              <td>{{ item.addedOn }}</td>
            </tr>
          </tbody>
        </template>
      </v-simple-table>
    </div>
    <div class="d-flex justify-content-center mb-5">
      <v-simple-table>
        <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">İsim</th>
              <th class="text-left">Kayıt Tarihi</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in doctors" :key="item.name">
              <td>{{ item.name }}</td>
              <td>{{ item.addedOn }}</td>
            </tr>
          </tbody>
        </template>
      </v-simple-table>
    </div>
    <div class="d-flex justify-content-center mb-5">
      <v-simple-table>
        <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">İsim</th>
              <th class="text-left">Kayıt Tarihi</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in polyclinics" :key="item.Name">
              <td>{{ item.name }}</td>
              <td>{{ item.addedOn }}</td>
            </tr>
          </tbody>
        </template>
      </v-simple-table>
    </div>
  </div>
</template>

<script>
import ApiService from "@/core/api.service.js";

export default {
  name: "Statistics",
  data() {
    return {
      doctors: [],
      patients: [],
      polyclinics: [],
    };
  },
  created() {
    this.getDoctors();
    this.getPatients();
    this.getPolyclinics();
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
};
</script>
