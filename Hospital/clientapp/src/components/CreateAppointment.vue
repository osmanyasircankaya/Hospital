<template>
  <v-app style="background-image: url(background.png); background-size: cover">
    <h1>RANDEVU AL</h1>
    <v-form>
      <v-container fill-height>
        <v-row justify="center" align="center">
          <v-col cols="12" sm="4">
            <v-select
              v-model="polId"
              :item-text="(item) => item.id"
              :items="polyclinics"
              label="Poliklinik"
              solo
              v-on:change="() => getDoctors()"
            >
              <template slot="selection" slot-scope="data">{{
                data.item.name
              }}</template>
              <template slot="item" slot-scope="data">{{
                data.item.name
              }}</template></v-select
            >
          </v-col>
          <v-col cols="12" sm="4">
            <v-select
              v-model="doctorId"
              :item-text="(item) => item.id"
              :items="doctors"
              label="Doktor"
              solo
            >
              <template slot="selection" slot-scope="data"
                >{{ data.item.firstName }} {{ data.item.lastName }}
              </template>
              <template slot="item" slot-scope="data"
                >{{ data.item.firstName }} {{ data.item.lastName }}</template
              ></v-select
            >
          </v-col>
        </v-row>
        <v-row justify="center" align="center">
          <v-col cols="12" sm="4">
            <v-datetime-picker
              label="Traih Seçiniz"
              v-model="AppointmentDate"
              :date-picker-props="dateProps"
              :time-picker-props="timeProps"
            >
              <template slot="dateIcon"> Tarih </template>
              <template slot="timeIcon"> Saat </template>
            </v-datetime-picker>
          </v-col>
        </v-row>
      </v-container>
      <v-btn class="mr-4" color="primary" type="submit" @click="submit()">
        submit
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
      userId: "",
      polId: 0,
      polyclinics: [],
      doctors: [],
      doctorId: 0,
      Appointment: {
        AppointmentDate: null,
        IsEmpty: false,
        DoctorId: 0,
        PatientId: 0,
      },
      Appointments: [],
      AppointmentDate: new Date(),
      dateProps: {
        headerColor: "blue",
        min: new Date().toISOString().substr(0, 10),
        max: "2021-07-18",
      },
      timeProps: {
        useSeconds: false,
        ampmInTitle: false,
        allowedHours: this.allowedHours,
        allowedMinutes: this.allowedStep,
        format: "24hr",
        min: "9:00",
        max: "17:00",
      },
    };
  },
  created() {
    this.getPolyclinics();
    this.getUserId();
  },
  methods: {
    allowedHours: (v) => v >= 8 || v < 17,
    allowedStep: (m) => m % 15 === 0,
    submit() {
      this.createAppointment();
      this.$router.push("Appointments");
    },
    getUserId() {
      this.userId = Vue.prototype.$userId;
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
    getDoctors() {
      ApiService.setHeader();
      ApiService.get("api/Doctor/GetAllByPolId", this.polId)
        .then((response) => {
          this.doctors = response.data;
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
    createAppointment() {
      (this.Appointment.AppointmentDate = this.AppointmentDate),
      (this.Appointment.DoctorId = this.doctorId),
      (this.Appointment.PatientId = this.userId),
      ApiService.setHeader();
      ApiService.put("api/Appointment", this.Appointment).catch(
        ({ response }) => {
          ApiService.showError(response);
        }
      );
    },
  },
};
</script>
