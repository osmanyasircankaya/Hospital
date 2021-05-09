import Vue from 'vue'
import Router from 'vue-router'
import HelloWorld from "@/components/HelloWorld.vue";
import Counter from "@/components/Counter.vue";

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: "/",
      name: "hello-world",
      component: HelloWorld,
    },
    {
      path: "/counter",
      name: "counter",
      component: Counter,
    },
  ]
})
