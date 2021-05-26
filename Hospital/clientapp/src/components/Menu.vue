<template>
  <div clas="d-flex justify-content-center">
    <h1>MENÜ</h1>
    <div>
      <b-button class="mt-5" to="/CreateAppointment" variant="primary" size="lg"
        >Randevu Al</b-button
      >
    </div>
    <div>
      <b-button class="mt-5" to="/Appointments" variant="primary" size="lg"
        >Randevularım</b-button
      >
    </div>
    <div>
      <b-button class="mt-5" to="/DeleteAppointment" variant="primary" size="lg"
        >Randevunu İptal Et</b-button
      >
    </div>
    <div>
      <b-button class="mt-5" to="/Statistics" variant="primary" size="lg"
        >İstatistikler</b-button
      >
    </div>
  </div>
</template>

<script>
export default {
  name: "Menu",
  data() {
    return {
      count: 0,
    };
  },
};
</script>
