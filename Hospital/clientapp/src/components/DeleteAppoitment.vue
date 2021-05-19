<template>
  <v-app>
    <h1>RANDEVU İPTAL ET</h1>
    <v-container fill-height>
      <v-row justify="center" align="center">
        <v-col cols="12" sm="4">
          <v-select
            v-model="appoitmentId"
            :item-text="(item) => item.id"
            :items="appoitments"
            label="Randevu"
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
  </v-app>
</template>

<script>
import ApiService from "@/core/api.service.js";
import Vue from "vue";

export default {
  name: "CreateAppoitment",
  data() {
    return {
      appoitmentId: 0,
      appoitments: [],
      userId: 0,
    };
  },
  created() {
    this.getUserId();
  },
  methods: {
    getAppoitments() {
      ApiService.setHeader();
      ApiService.get("api/Appoitment", this.userId)
        .then((response) => {
          this.appoitments = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },
    deleteAppoitment() {
      ApiService.setHeader();
      ApiService.delete("api/Appoitment/" + this.editedItem.Id)
        .then(() => {
          this.getAppoitments();
        })
        .catch(function (error) {
          alert(error);
        });
    },
    getUserId() {
      this.userId = Vue.prototype.$userId
      this.getAppoitments();
    },
  },
};
</script>
