<template>
  <div>
    <h1 class="mb-5" style="color: #FFFFFF">İSTATİSTİKLER</h1>
    <!-- <div class="d-flex justify-content-center mt-5 mb-5">
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
    </div> -->
  </div>
</template>

<script>
import ApiService from "@/core/api.service.js";

export default {
  name: "Statistics",
  data() {
    return {
      appointmensCountOrderByDate: [],
      doctorsOrderByAppointmentCount: [],
      maximumAppointmentDay: {},
      minimumAppointmentDay: {},
      polyclinicNameByAppointmentCount: [],
      weekDayByAppointmentCount: [],
      appointmentCountByHours: [],
      appointmentsSizeByDoctorId: 0,
      appointmentsDetailByDateRange: []
    };
  },
  created() {
    this.getAppointmensCountOrderByDate()
    this.getDoctorsOrderByAppointmentCount()
    this.getMaximumAppointmentDay()
    this.getMinimumAppointmentDay()
    this.getPolyclinicNameByAppointmentCount()
    this.getWeekDayByAppointmentCount()
    this.getAppointmentCountByHours()
    this.getAppointmentsSizeByDoctorId()
    this.getAppointmentsDetailByDateRange()
  },
  methods: {
    getAppointmensCountOrderByDate() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetAppointmentsCountOrderByDate")
        .then((response) => {
          this.appointmensCountOrderByDate = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getDoctorsOrderByAppointmentCount() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetDoctorsOrderByAppointmentCount")
        .then((response) => {
          this.doctorsOrderByAppointmentCount = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getMaximumAppointmentDay() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetMaximumAppointmentDay")
        .then((response) => {
          this.maximumAppointmentDay = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getMinimumAppointmentDay() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetMinimumAppointmentDay")
        .then((response) => {
          this.minimumAppointmentDay = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getPolyclinicNameByAppointmentCount() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetPolyclinicNameByAppointmentCount")
        .then((response) => {
          this.polyclinicNameByAppointmentCount = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getWeekDayByAppointmentCount() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetWeekDayByAppointmentCount")
        .then((response) => {
          this.weekDayByAppointmentCount = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getAppointmentsSizeByDoctorId() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetAppointmentsSizeByDoctorId/" + 3 + "/" + 30)
        .then((response) => {
          this.appointmentsSizeByDoctorId = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getAppointmentsDetailByDateRange() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetAppointmentsDetailByDateRange/" + "21443134132" + "/" + 30)
        .then((response) => {
          this.appointmentsDetailByDateRange = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getAppointmentCountByHours() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/getAppointmentCountByHours/" + 9 + "/" + 17)
        .then((response) => {
          this.appointmentCountByHours = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },
  },
};
</script>
