<template>
  <v-app style="background-image: url(background.png); background-size: cover">
    <h1>RANDEVU İPTAL ET</h1>
    <v-form>
    <v-container fill-height>
      <v-row justify="center" align="center">
        <v-col cols="12" sm="4">
          <span>RANDEVU SEÇ</span>
          <v-select
            v-model="AppointmentId"
            :item-text="(item) => item.id"
            :items="Appointments"
            label="Randevu"
            class="mt-2"
            solo
          >
            <template slot="selection" slot-scope="data">{{
              data.item.id
            }}</template>
            <template slot="item" slot-scope="data">{{
              data.item.id
            }}</template></v-select
          >
        </v-col>
      </v-row>
    </v-container>
    <v-btn class="mr-4" color="primary" type="submit" @click="submit()">
        İPTAL ET
      </v-btn>
    </v-form>
  </v-app>
</template>

<script>
import ApiService from "@/core/api.service.js";
import Vue from "vue";

export default {
  name: "CreateAppointment",
  data() {
    return {
      AppointmentId: 0,
      Appointments: [],
      userId: '',
    };
  },
  created() {
    this.getUserId();
  },
  methods: {
    submit() {
      this.deleteAppointment();
      this.$router.push("Appointments");
    },
    getAppointments() {
      ApiService.setHeader();
      ApiService.get("api/Appointment/GetAllByPatientId", this.userId)
        .then((response) => {
          if(response.data == null){
            alert("randevu yok")
          }
          this.Appointments = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },
    deleteAppointment() {
      ApiService.setHeader();
      ApiService.delete("api/Appointment/DeleteAppointmentByPatientId/" + this.userId)
        .then(() => {
          //this.getAppointments();
        })
        .catch(function (error) {
          alert(error);
        });
    },
    getUserId() {
      this.userId = Vue.prototype.$userId
      this.getAppointments();
    },
  },
};
</script>
