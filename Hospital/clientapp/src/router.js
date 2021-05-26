import Vue from 'vue'
import Router from 'vue-router'
import Home from "@/components/Home.vue";
import Login from "@/components/Login.vue";
import Menu from "@/components/Menu.vue";
import CreateAppointment from "@/components/CreateAppointment.vue";
import Appointments from "@/components/Appointments.vue";
import DeleteAppointment from "@/components/DeleteAppointment.vue";
import Statistics from "@/components/Statistics.vue";

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: "/",
      name: "Home",
      component: Home,
    },
    {
      path: "/Login",
      name: "Login",
      component: Login,
    },
    {
      path: "/Menu",
      name: "Menu",
      component: Menu,
    },
    {
      path: "/CreateAppointment",
      name: "CreateAppointment",
      component: CreateAppointment,
    },
    {
      path: "/Appointments",
      name: "Appointments",
      component: Appointments,
    },
    {
      path: "/DeleteAppointment",
      name: "DeleteAppointment",
      component: DeleteAppointment,
    },
    {
      path: "/Statistics",
      name: "Statistics",
      component: Statistics,
    },
  ]
})
