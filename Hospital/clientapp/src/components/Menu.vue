<template>
  <div clas="d-flex justify-content-center">
    <h1 class="mb-5" style="color: #FFFFFF">MENÜ</h1>
    <div>
      <b-button class="mt-5" to="/CreateAppointment" variant="outline-light" size="lg" style="width: 250px"
        >Randevu Al</b-button
      >
    </div>
    <div>
      <b-button class="mt-5" to="/DeleteAppointment" variant="outline-light" size="lg" style="width: 250px"
        >Randevunu İptal Et</b-button
      >
    </div>
    <div>
      <b-button class="mt-5" to="/Statistics" variant="outline-light" size="lg" style="width: 250px"
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
