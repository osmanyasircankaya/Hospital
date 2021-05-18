<template>
  <v-app>
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
  name: "CreateAppoitment",
  data() {
    return {
      appoitmentId: 0,
      appoitments: [],
    };
  },
  created() {
    //this.getAppoitments();
  },
  methods: {
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
