<template>
  <div clas="d-flex justify-content-center">
    <div>
      <b-button class="mt-5" to="/CreateAppoitment" variant="primary" size="lg"
        >Randevu Al</b-button
      >
    </div>
    <div>
      <b-button class="mt-5" to="/DeleteAppoitment" variant="primary" size="lg"
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
        
    },
}
</script>
