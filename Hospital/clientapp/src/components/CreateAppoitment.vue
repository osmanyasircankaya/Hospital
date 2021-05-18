<template>
  <v-app>
    <h1>RANDEVU AL</h1>
    <v-form>
      <v-container fill-height>
        <v-row justify="center" align="center">
          <v-col cols="12" sm="4">
            <v-select
              v-model="polyclinicId"
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
              <template slot="selection" slot-scope="data">{{
                data.item.name
              }}</template>
              <template slot="item" slot-scope="data">{{
                data.item.name
              }}</template></v-select
            >
          </v-col>
        </v-row>
        <v-row justify="center" align="center">
          <v-col cols="12" sm="4">
            <v-date-picker
              v-model="date"
              class="mt-4"
              min="2021-05-18"
              max="2021-07-18"
            ></v-date-picker>
          </v-col>
          <v-col cols="12" sm="4">
            <v-time-picker
              v-model="timeStep"
              :allowed-hours="allowedHours"
              :allowed-minutes="allowedStep"
              class="mt-4"
              scrollable
              min="8:30"
              max="17:30"
            ></v-time-picker>
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

export default {
  name: "CreateAppoitment",
  data() {
    return {
      polyclinicId: 0,
      polyclinics: [],
      doctors: [],
      doctorId: 0,
      availableDays: [],
      availableHours: [],
      appoitments: [],
      date: "2021-05-18",
      time: "11:15",
      timeStep: "08:30",
    };
  },
  created() {
    //this.getPolyclinics();
  },
  methods: {
    allowedHours: (v) => v >= 8 || v < 6,
    allowedMinutes: (v) => v == 15,
    allowedStep: (m) => m % 15 === 0,
    submit() {
      alert("submit");
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
      ApiService.get("api/Doctor")
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
  },
};
</script>
