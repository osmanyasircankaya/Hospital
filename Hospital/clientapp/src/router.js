import Vue from 'vue'
import Router from 'vue-router'
import Home from "@/components/Home.vue";
import Login from "@/components/Login.vue";
import Menu from "@/components/Menu.vue";
import CreateAppoitment from "@/components/CreateAppoitment.vue";
import DeleteAppoitment from "@/components/DeleteAppoitment.vue";
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
      path: "/CreateAppoitment",
      name: "CreateAppoitment",
      component: CreateAppoitment,
    },
    {
      path: "/DeleteAppoitment",
      name: "DeleteAppoitment",
      component: DeleteAppoitment,
    },
    {
      path: "/Statistics",
      name: "Statistics",
      component: Statistics,
    },
  ]
})
