import { createRouter, createWebHistory } from 'vue-router';
import DefaultLayout from '@/layouts/DefaultLayout.vue';
import AnalyticsPage from '@/pages/AnalyticsPage.vue';
import BookDetailsPage from '@/pages/BookDetailsPage.vue';
import MyBooksPage from '@/pages/MyBooksPage.vue';
import SettingsPage from '@/pages/SettingsPage.vue';

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      component: DefaultLayout,
      children: [
        { path: '', redirect: '/books' },
        { path: 'books', component: MyBooksPage, meta: { title: 'My Books' } },
        { path: 'books/:id', component: BookDetailsPage, props: true, meta: { title: 'Book Details' } },
        { path: 'analytics', component: AnalyticsPage, meta: { title: 'Analytics' } },
        { path: 'settings', component: SettingsPage, meta: { title: 'Settings' } },
      ],
    },
  ],
});

export default router;
