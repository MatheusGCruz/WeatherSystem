import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Current from '../views/Current.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/about',
      name: 'about',
      component: () => import('../views/AboutView.vue'),
    },
    {
      path: '/analytics',
      name: 'analytics',
      component: () => import('../views/Analytics.vue'),
    },
    {
      path: '/:subdirectory',
      name: 'Weather',
      component: Current,
      props: true, 
    },
  ],
})

export default router
