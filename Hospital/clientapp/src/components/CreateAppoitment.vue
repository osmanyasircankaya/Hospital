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
              v-model="appoitmentDate"
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
  name: "CreateAppoitment",
  data() {
    return {
      userId: "",
      polId: 0,
      polyclinics: [],
      doctors: [],
      doctorId: 0,
      appoitment: {
        AppoitmentDate: null,
        IsEmpty: false,
        DoctorId: 0,
        PatientId: 0,
      },
      appoitments: [],
      appoitmentDate: new Date(),
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
      this.createAppoitment();
      this.$router.push("Appoitments");
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
    getAppoitments() {
      ApiService.setHeader();
      ApiService.get("api/Appoitment")
        .then((response) => {
          this.appoitments = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },
    createAppoitment() {
      (this.appoitment.AppoitmentDate = this.appoitmentDate),
      (this.appoitment.DoctorId = this.doctorId),
      (this.appoitment.PatientId = this.userId),
      ApiService.setHeader();
      ApiService.put("api/Appoitment", this.appoitment).catch(
        ({ response }) => {
          ApiService.showError(response);
        }
      );
    },
  },
};
</script>
