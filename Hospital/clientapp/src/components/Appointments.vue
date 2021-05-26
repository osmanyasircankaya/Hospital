<template>
  <v-app style="background-image: url(background.png); background-size: cover">
    <h1>RANDEVULARIM</h1>
    <v-container fill-height>
      <v-row justify="center" align="center">
        <v-col cols="12" sm="4">
        </v-col>
      </v-row>
    </v-container>
  </v-app>
</template>

<script>
import ApiService from "@/core/api.service.js";

export default {
  name: "Appointments",
  data() {
    return {
      AppointmentId: 0,
      Appointments: [],
    };
  },
  created() {
    this.getAppointments();
  },
  methods: {
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
