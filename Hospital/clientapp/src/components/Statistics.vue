<template>
  <div>
    <h1 class="mb-5" style="color: #FFFFFF">İSTATİSTİKLER</h1>
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
              <td>{{ item.id }}</td>
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
              <th class="text-left">Soyisim</th>
              <th class="text-left">Kayıt Tarihi</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in doctors" :key="item.name">
              <td>{{ item.firstName }}</td>
              <td>{{ item.lastName }}</td>
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
    <div class="d-flex justify-content-center mb-5">
      <v-simple-table>
        <template v-slot:default>
          <thead>
            <tr>
              <th class="text-left">Tarih</th>
              <th class="text-left">Doktor</th>
              <th class="text-left">Hasta</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in Appointments" :key="item.id">
              <td>{{ item.appointmentDate }}</td>
              <td>{{ item.doctorId }}</td>
              <td>{{ item.patientId }}</td>
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
      Appointments: []
    };
  },
  created() {
    this.getDoctors();
    this.getPatients();
    this.getPolyclinics();
    this.getAppointments();
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
    getAppointments() {
      ApiService.setHeader();
      ApiService.get("api/Appointment")
        .then((response) => {
          this.Appointments = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },
  },
};
</script>
