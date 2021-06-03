<template>
  <v-app style="background-image: url(background.png); background-size: cover">
    <h1 class="mb-5" style="color: #ffffff">RANDEVU AL</h1>
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
        <v-row justify="center" align="center">
          <v-col cols="12" sm="4">
            <v-text-field
              v-model="Patient.Mail"
              :rules="emailRules"
              solo
              label="E-Mail Adresi"
            ></v-text-field>
          </v-col>
        </v-row>
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
        </v-row>
        <v-row justify="center" align="center">
          <v-col cols="12" sm="4">
            <v-select
              v-model="Appointment.DoctorId"
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
            <div style="backgroundcolor: #ffffff">
              <v-datetime-picker
                label="Tarih Seçiniz"
                v-model="Appointment.AppointmentDate"
                :date-picker-props="dateProps"
                :time-picker-props="timeProps"
              >
                <template slot="dateIcon"> Tarih </template>
                <template slot="timeIcon"> Saat </template>
              </v-datetime-picker>
            </div>
          </v-col>
        </v-row>
      </v-container>
      <v-btn class="mr-4" color="success" type="submit" @click="submit()">
        RANDEVU AL
      </v-btn>
    </v-form>
  </v-app>
</template>

<script>
import ApiService from "@/core/api.service.js";

export default {
  name: "CreateAppointment",
  data() {
    return {
      polId: 0,
      polyclinics: [],
      doctors: [],
      appointmentDates: [],
      Appointment: {
        AppointmentDate: null,
        IsEmpty: false,
        DoctorId: 0,
        PatientId: "",
      },
      Patient: {
        Id: "",
      },
      Appointments: [],
      dateProps: {
        headerColor: "blue",
        min: new Date().toISOString().substr(0, 10),
        max: "2021-07-18",
        showCurrent: true
      },
      timeProps: {
        useSeconds: false,
        ampmInTitle: false,
        allowedHours: this.allowedHours,
        allowedMinutes: this.allowedStep,
        format: "24hr",
        min: "9:00",
        max: "16:45",
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
      emailRules: [
        (v) =>
          !v ||
          /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
          "Lütfen geçerli bir E-mail adresi giriniz",
      ],
    };
  },
  created() {
    this.getPolyclinics();
    this.getAppointments();
  },
  methods: {
    allowedHours: (v) => (v >= 8 || v <= 16) && v != 12,

    allowedStep: (m) => m % 15 === 0,

    submit() {
      this.checkDate();
      if (this.Patient.Id.length === 11) {
        this.createPatient();
        setTimeout(() => {
          this.createAppointment();
        }, 1000);
        //this.$router.push("Menu");
      } else {
        alert("Kimlik numarası 11 haneli olmak zorunda");
      }
    },

    checkDate() {
      this.appointmentDates.forEach((element) => {
        if (element === this.Appointment.AppointmentDate) {
          alert("Bu tarih ve saat seçilemez");
        }
      });
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
          this.Appointments.forEach((element, index) => {
            this.appointmentDates[index] = element.appointmentDate;
          });
        })
        .catch(function (error) {
          alert(error);
        });
    },

    createPatient() {
      ApiService.setHeader();
      ApiService.put("api/Patient", this.Patient).catch(({ response }) => {
        ApiService.showError(response);
      });
    },

    createAppointment() {
      (this.Appointment.PatientId = this.Patient.Id), ApiService.setHeader();
      ApiService.put("api/Appointment", this.Appointment).catch(
        ({ response }) => {
          ApiService.showError(response);
        }
      );
    },
  },
};
</script>
