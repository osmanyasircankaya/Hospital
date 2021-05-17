<template>
  <v-app>
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
        <!-- <v-col cols="12" sm="4">
          <v-select
            v-model="polyclinic"
            :item-text="(item) => item.id"
            :items="availableDays"
            label="Gün"
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
        <v-col cols="12" sm="4">
          <v-select
            v-model="polyclinic"
            :item-text="(item) => item.id"
            :items="doctors"
            label="Saat"
            solo
          >
            <template slot="selection" slot-scope="data">{{
              data.item.name
            }}</template>
            <template slot="item" slot-scope="data">{{
              data.item.name
            }}</template></v-select
          >
        </v-col> -->
      </v-row>
    </v-container>
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
      appoitments: []
    };
  },
  created() {
    this.getPolyclinics();
  },
  methods: {
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
