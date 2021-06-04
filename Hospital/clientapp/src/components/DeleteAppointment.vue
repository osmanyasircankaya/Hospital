<template>
  <v-app style="background-image: url(background.png); background-size: cover">
    <h1 class="mb-5" style="color: #ffffff">RANDEVU İPTAL ET</h1>
    <v-form>
      <v-container fill-height>
        <v-row justify="center" align="center">
          <v-col cols="12" sm="4">
            <v-text-field
              v-model="Patient.Id"
              :rules="rules"
              minlength="11"
              solo
              label="Kimlik Numarası"
              v-mask="mask"
            ></v-text-field>
          </v-col>
        </v-row>
      </v-container>
      <v-btn class="mr-4" color="error" type="submit" @click="exit()">
        MENÜYE DÖN
      </v-btn>
      <v-btn color="outline-light" @click="getAppointments()">
        RANDEVULARI GETİR
      </v-btn>
    </v-form>
    <v-form v-if="Appointments.length != 0">
      <v-row class="mt-8" justify="center" align="center">
        <v-col cols="12" sm="4">
          <h5 class="mt-3" style="color: #ffffff">RANDEVU SEÇ</h5>
          <v-select
            :item-text="(item) => item.id"
            :items="Appointments"
            label="Randevu"
            class="mt-2"
            solo
          >
            <template slot="selection" slot-scope="data">{{
              data.item.appointmentDate | moment
            }}</template>
            <template slot="item" slot-scope="data">{{
              data.item.appointmentDate | moment
            }}</template></v-select
          >
        </v-col>
      </v-row>
      <v-btn class="mt-2" color="outline-light" @click="submit()">
        RANDEVUYU İPTAL ET
      </v-btn>
    </v-form>
  </v-app>
</template>

<script>
import ApiService from "@/core/api.service.js";
import moment from "moment";

export default {
  name: "CreateAppointment",
  data() {
    return {
      Appointments: [],
      Patient: {
        Id: "",
      },
      mask: [
        /[1-9]/,
        /[0-9]/,
        /[0-9]/,
        /[0-9]/,
        /[0-9]/,
        /[0-9]/,
        /[0-9]/,
        /[0-9]/,
        /[0-9]/,
        /[0-9]/,
        /[0-9]/,
      ],
      rules: [
        (v) => v.length === 11 || "Kimlik numarası 11 karakterli olmalı !",
      ],
    };
  },

  filters: {
    moment: function (date) {
      return moment(date).add(3, "h").locale("tr").format("LLL");
    },
  },

  mounted(){
    this.getDoctors()
  },

  methods: {
    submit() {
      if (this.Patient.Id.length === 11) {
        this.deleteAppointment();
        this.$router.push("Menu");
      } else {
        alert("Kimlik numarası 11 haneli olmak zorunda");
      }
    },
    
    exit() {
      this.$router.push("Menu");
    },

    getAppointments() {
      ApiService.setHeader();
      ApiService.get("api/Appointment/GetAllByPatientId", this.Patient.Id)
        .then((response) => {
          if (response.data.length === 0) {
            alert("Bu kimlik numarasına ait randevu bulunamadı.");
            return;
          }
          this.Appointments = response.data
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

    deleteAppointment() {
      ApiService.setHeader();
      ApiService.delete(
        "api/Appointment/DeleteAppointmentByPatientId/" + this.Patient.Id
      ).catch(function (error) {
        alert(error);
      });
    },
  },
};
</script>
