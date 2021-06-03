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
                <td>{{ item.Date }}</td>
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
            <v-select></v-select>
            <span>{{ appointmentsSizeByDoctorId }}</span>
          </v-col>
        </div>
      </v-dialog>
    </div>
    <div class="d-flex justify-content-center mt-5 mb-5">
      <v-dialog v-model="dialog7" width="600px">
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
                <td>{{ item.AppointmentDate }}</td>
                <td>{{ item.DoctorName }}</td>
                <td>{{ item.PolyclinicName }}</td>
              </tr>
            </tbody>
          </template>
        </v-simple-table>
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
            {{ maximumAppointmentDay.Date }}:
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
            {{ minimumAppointmentDay.Date }}:
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
    this.getAppointmentsDetailByDateRange();
    this.getAppointmentsCount();
  },
  methods: {
    exit() {
      this.$router.push("Menu");
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
        })
        .catch(function (error) {
          alert(error);
        });
    },

    getAppointmentsSizeByDoctorId() {
      ApiService.setHeader();
      ApiService.get(
        "api/Statistic/GetAppointmentsSizeByDoctorId/" + 3 + "/" + 30
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
          "21443134132" +
          "/" +
          30
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
      ApiService.get("api/Statistic/getAppointmentCountByHours/" + 9 + "/" + 17)
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
  },
};
</script>
