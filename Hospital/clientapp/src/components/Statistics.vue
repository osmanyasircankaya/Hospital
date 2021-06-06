<template>
  <v-app style="background-image: url(background.png); background-size: cover">
    <h1 class="mb-5" style="color: #ffffff">İSTATİSTİKLER</h1>
    <div class="d-flex justify-content-center mt-5 mb-5">
      <v-dialog v-model="dialog1" width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            class="mr-5"
            v-on="on"
            width="500px"
          >
            Randevu Sayısına Göre Tarihlerin Sıralaması
          </v-btn>
        </template>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">Tarih</th>
                <th class="text-left">Randevu Adedi</th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(item, index) in appointmensCountOrderByDate"
                :key="index"
              >
                <td>{{ item.Date | moment }}</td>
                <td>{{ item.AppointmentCount }}</td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-dialog>
      <v-dialog v-model="dialog2" width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            v-on="on"
            width="500px"
          >
            En Çok Randevusu Olan Doktorlar
          </v-btn>
        </template>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">Doktor</th>
                <th class="text-left">Randevu Adedi</th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(item, index) in doctorsOrderByAppointmentCount"
                :key="index"
              >
                <td>{{ item.FullName }}</td>
                <td>{{ item.AppointmentCount }}</td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-dialog>
    </div>
    <div class="d-flex justify-content-center mt-5 mb-5">
      <v-dialog v-model="dialog3" width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            class="mr-5"
            v-on="on"
            width="500px"
          >
            Randevu Sayısına Göre Poliklinikler
          </v-btn>
        </template>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">Poliklinik Adı</th>
                <th class="text-left">Randevu Adedi</th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(item, index) in polyclinicNameByAppointmentCount"
                :key="index"
              >
                <td>{{ item.Name }}</td>
                <td>{{ item.AppointmentCount }}</td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-dialog>
      <v-dialog v-model="dialog4" width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            v-on="on"
            width="500px"
          >
            Randevu Sayısına Göre Günler
          </v-btn>
        </template>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">Gün</th>
                <th class="text-left">Randevu Adedi</th>
              </tr>
            </thead>
            <tbody>
              <tr
                v-for="(item, index) in weekDayByAppointmentCount"
                :key="index"
              >
                <td>{{ item.GUN }}</td>
                <td>{{ item.AppointmentCount }}</td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-dialog>
    </div>
    <div class="d-flex justify-content-center mt-5 mb-5">
      <v-dialog v-model="dialog5" width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            class="mr-5"
            v-on="on"
            width="500px"
          >
            Saatlere Göre Randevu Sayıları
          </v-btn>
        </template>
        <v-simple-table>
          <template v-slot:default>
            <thead>
              <tr>
                <th class="text-left">Saat</th>
                <th class="text-left">Randevu Adedi</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="(item, index) in appointmentCountByHours" :key="index">
                <td>{{ item.HOURS }}</td>
                <td>{{ item.AppointmentCount }}</td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-dialog>
      <v-dialog v-model="dialog6" width="300px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            v-on="on"
            width="500px"
          >
            Doktorlara Göre Randevu Sayıları
          </v-btn>
        </template>
        <div style="background-color: #ffffff; height: 150px">
          <v-col cols="12" sm="12">
            <v-select
              v-model="doctorId"
              :item-text="(item) => item.id"
              :items="doctors"
              label="Doktor"
              solo
              v-on:change="() => getAppointmentsSizeByDoctorId()"
            >
              <template slot="selection" slot-scope="data"
                >{{ data.item.firstName }} {{ data.item.lastName }}</template
              >
              <template slot="item" slot-scope="data"
                >{{ data.item.firstName }} {{ data.item.lastName }}</template
              ></v-select
            >
            <span>Randevu Sayısı: {{ appointmentsSizeByDoctorId }}</span>
          </v-col>
        </div>
      </v-dialog>
    </div>
    <div class="d-flex justify-content-center mt-5 mb-5">
      <v-dialog v-model="dialog7" width="700px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            class="mr-5"
            v-on="on"
            width="500px"
          >
            Seçilen Hastanın Randevuları
          </v-btn>
        </template>
        <div style="background-color: #ffffff">
          <v-row justify="center" align="center">
            <v-col>
              <v-select
                v-model="patientId"
                :item-text="(item) => item.id"
                :items="patients"
                label="Hasta"
                solo
                v-on:change="() => getAppointmentsDetailByDateRange()"
              >
                <template slot="selection" slot-scope="data">{{
                  data.item.id
                }}</template>
                <template slot="item" slot-scope="data">{{
                  data.item.id
                }}</template></v-select
              >
            </v-col>
            <v-col>
              <v-select
                v-model="dayValue"
                :item-text="(item) => item.value"
                :items="items"
                label="Filtre"
                solo
                v-on:change="() => getAppointmentsDetailByDateRange()"
              >
                <template slot="selection" slot-scope="data">{{
                  data.item.name
                }}</template>
                <template slot="item" slot-scope="data">{{
                  data.item.name
                }}</template
                >></v-select
              >
            </v-col>
          </v-row>
          <v-simple-table>
            <template v-slot:default>
              <thead>
                <tr>
                  <th class="text-left">Randevu Tarihi</th>
                  <th class="text-left">Doktor Adı</th>
                  <th class="text-left">Poliklinik Adı</th>
                </tr>
              </thead>
              <tbody>
                <tr
                  v-for="(item, index) in appointmentsDetailByDateRange"
                  :key="index"
                >
                  <td>{{ item.AppointmentDate | momentWithHour }}</td>
                  <td>{{ item.DoctorName }}</td>
                  <td>{{ item.PolyclinicName }}</td>
                </tr>
              </tbody>
            </template>
          </v-simple-table>
        </div>
      </v-dialog>
      <v-dialog v-model="dialog8" width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            v-on="on"
            width="500px"
          >
            Toplam Randevu Sayısı
          </v-btn>
        </template>
        <div style="background-color: #ffffff; height: 50px">
          <h5>{{ appointmentsCount }} Randevu</h5>
        </div>
      </v-dialog>
    </div>
    <div class="d-flex justify-content-center mt-5 mb-5">
      <v-dialog v-model="dialog9" width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            class="mr-5"
            v-on="on"
            width="500px"
          >
            En Çok Randevu Alınan Tarih
          </v-btn>
        </template>
        <div style="background-color: #ffffff; height: 50px">
          <h5>
            {{ maximumAppointmentDay.Date | moment }}:
            {{ maximumAppointmentDay.AppointmentCount }} Randevu
          </h5>
        </div>
      </v-dialog>
      <v-dialog v-model="dialog10" width="600px">
        <template v-slot:activator="{ on, attrs }">
          <v-btn
            color="#ffffff"
            outlined
            v-bind="attrs"
            v-on="on"
            width="500px"
          >
            En Az Randevu Alınan Tarih
          </v-btn>
        </template>
        <div style="background-color: #ffffff; height: 50px">
          <h5>
            {{ minimumAppointmentDay.Date | moment }}:
            {{ minimumAppointmentDay.AppointmentCount }} Randevu
          </h5>
        </div>
      </v-dialog>
    </div>
    <div class="d-flex justify-content-center mt-5 mb-5">
      <v-btn color="error" type="submit" @click="exit()"> MENÜYE DÖN </v-btn>
    </div>
  </v-app>
</template>

<script>
import ApiService from "@/core/api.service.js";
import moment from "moment";

export default {
  name: "Statistics",
  data() {
    return {
      dialog1: false,
      dialog2: false,
      dialog3: false,
      dialog4: false,
      dialog5: false,
      dialog6: false,
      dialog7: false,
      dialog8: false,
      dialog9: false,
      dialog10: false,
      appointmensCountOrderByDate: [],
      doctorsOrderByAppointmentCount: [],
      maximumAppointmentDay: {},
      minimumAppointmentDay: {},
      polyclinicNameByAppointmentCount: [],
      weekDayByAppointmentCount: [],
      appointmentCountByHours: [],
      appointmentsSizeByDoctorId: 0,
      appointmentsDetailByDateRange: [],
      appointmentsCount: 0,
      patients: [],
      patientId: "",
      doctors: [],
      doctorId: 0,
      items: [
        { name: "Son 7 Gün", value: 7 },
        { name: "Son 15 Gün", value: 15 },
        { name: "Son 1 Ay", value: 30 },
        { name: "Son 3 Ay", value: 90 },
      ],
      dayValue: 365
    };
  },

  created() {
    this.getAppointmensCountOrderByDate();
    this.getDoctorsOrderByAppointmentCount();
    this.getMaximumAppointmentDay();
    this.getMinimumAppointmentDay();
    this.getPolyclinicNameByAppointmentCount();
    this.getWeekDayByAppointmentCount();
    this.getAppointmentCountByHours();
    this.getAppointmentsSizeByDoctorId();
    this.getAppointmentsCount();
    this.getPatients();
    this.getDoctors();
  },

  filters: {
    moment: function (date) {
      return moment(date).locale("tr").format("LL");
    },

    momentWithHour: function (date) {
      return moment(date).add(3, "h").locale("tr").format("LLL");
    },
  },

  methods: {
    exit() {
      this.$router.push("Menu");
    },

    localization() {
      this.weekDayByAppointmentCount.forEach((element) => {
        if (element.GUN === "Monday") {
          element.GUN = "Pazartesi";
        } else if (element.GUN === "Tuesday") {
          element.GUN = "Salı";
        } else if (element.GUN === "Wednesday") {
          element.GUN = "Çarşamba";
        } else if (element.GUN === "Thursday") {
          element.GUN = "Perşembe";
        } else if (element.GUN === "Friday") {
          element.GUN = "Cuma";
        } else if (element.GUN === "Saturday") {
          element.GUN = "Cumartesi";
        } else if (element.GUN === "Sunday") {
          element.GUN = "Pazar";
        } else {
          return;
        }
      });
    },

    getAppointmensCountOrderByDate() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetAppointmentsCountOrderByDate")
        .then((response) => {
          this.appointmensCountOrderByDate = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getDoctorsOrderByAppointmentCount() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetDoctorsOrderByAppointmentCount")
        .then((response) => {
          this.doctorsOrderByAppointmentCount = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getMaximumAppointmentDay() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetMaximumAppointmentDay")
        .then((response) => {
          this.maximumAppointmentDay = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getMinimumAppointmentDay() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetMinimumAppointmentDay")
        .then((response) => {
          this.minimumAppointmentDay = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getPolyclinicNameByAppointmentCount() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetPolyclinicNameByAppointmentCount")
        .then((response) => {
          this.polyclinicNameByAppointmentCount = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getWeekDayByAppointmentCount() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetWeekDayByAppointmentCount")
        .then((response) => {
          this.weekDayByAppointmentCount = response.data;
          this.localization();
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getAppointmentsSizeByDoctorId() {
      ApiService.setHeader();
      ApiService.get(
        "api/Statistic/GetAppointmentsSizeByDoctorId/" +
          this.doctorId +
          "/" +
          30
      )
        .then((response) => {
          this.appointmentsSizeByDoctorId = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getAppointmentsDetailByDateRange() {
      ApiService.setHeader();
      ApiService.get(
        "api/Statistic/GetAppointmentsDetailByDateRange/" +
          this.patientId +
          "/" +
          this.dayValue
      )
        .then((response) => {
          this.appointmentsDetailByDateRange = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getAppointmentCountByHours() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/getAppointmentCountByHours/" + 8 + "/" + 18)
        .then((response) => {
          this.appointmentCountByHours = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getAppointmentsCount() {
      ApiService.setHeader();
      ApiService.get("api/Statistic/GetAppointmentsCount")
        .then((response) => {
          this.appointmentsCount = response.data;
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getPatients() {
      ApiService.setHeader();
      ApiService.get("api/Patient")
        .then((response) => {
          this.patients = response.data;
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
  },
};
</script>
