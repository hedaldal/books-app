<template>
  <v-app class="app-shell">
    <v-navigation-drawer
      v-model="drawer"
      :location="mobile ? 'bottom' : undefined"
      class="sidebar"
      :class="mobile ? 'mobile-drawer' : ''"
    >
      <div class="sidebar-content">
        <template v-if="!mobile">
          <div class="d-flex mx-4 mt-5 mb-3">
            <v-icon color="#f8d54e" class="mr-2">mdi-book-open-page-variant</v-icon>
            <span class="sidebar-title">BookHub</span>
          </div>
          <v-divider></v-divider>
        </template>

        <v-list nav density="comfortable" bg-color="transparent" class="px-0">
          <v-list-item
            v-for="item in navItems"
            :key="item.to"
            :to="item.to"
            class="px-5"
            active-class="active-nav-item"
          >
            <div class="d-flex align-center">
              <v-icon :icon="item.icon" size="18" class="mr-3" />
              <v-list-item-title>{{ item.title }} </v-list-item-title>
            </div>
          </v-list-item>
        </v-list>

        <template v-if="!mobile">
          <v-spacer />
          <v-divider class="mx-4 my-3" />
          <v-list class="px-0 pb-4">
            <v-list-item class="px-5 user-item" rounded="lg">
              <template #prepend>
                <v-avatar size="32" color="white" class="mr-3">
                  <v-icon size="18" color="primary">mdi-account</v-icon>
                </v-avatar>
              </template>

              <v-list-item-title class="text-body-2"> Ezgi Ozturk </v-list-item-title>

              <template #append>
                <v-icon size="18">mdi-chevron-right</v-icon>
              </template>
            </v-list-item>
          </v-list>
        </template>
      </div>
    </v-navigation-drawer>

    <v-app-bar flat class="topbar">
      <v-app-bar-nav-icon :ripple="false" @click="toggleDrawer"></v-app-bar-nav-icon>
      <v-app-bar-title class="ml-2">{{ route.meta.title || 'Dashboard' }}</v-app-bar-title>
      <template v-if="mobile">
        <v-spacer />
        <v-avatar v-if="mobile" size="32" color="grey" class="mr-4 cursor-pointer">
          <v-icon size="18">mdi-account</v-icon>
        </v-avatar>
      </template>
    </v-app-bar>

    <v-main>
      <v-container class="py-6" style="max-width: 1000px">
        <router-view />
      </v-container>
    </v-main>
  </v-app>
</template>
<script setup lang="ts">
import { ref } from 'vue';
import { useRoute } from 'vue-router';
import { useDisplay } from 'vuetify';

const { mobile } = useDisplay();

const route = useRoute();
const drawer = ref(!mobile.value);

const navItems = [
  { title: 'My Books', to: '/books', icon: 'mdi-book-outline' },
  { title: 'Analytics', to: '/analytics', icon: 'mdi-chart-line' },
  { title: 'Settings', to: '/settings', icon: 'mdi-cog-outline' },
];

const toggleDrawer = () => {
  drawer.value = !drawer.value;
};
</script>

<style lang="scss" scoped>
.app-shell {
  background: #f3f5fb;

  .sidebar {
    background: linear-gradient(180deg, #4a63f6 0%, #3f58e3 100%);
    color: #f7f8ff;

    &.mobile-drawer {
      height: auto !important;
    }

    .sidebar-content {
      height: 100%;
      display: flex;
      flex-direction: column;

      .sidebar-title {
        font-size: 20px;
        font-weight: 600;
      }

      .active-nav-item {
        position: relative;
        &:before {
          content: '';
          position: absolute;
          left: 0;
          top: 1px;
          bottom: 1px;
          width: 4px;
          background-color: #f8d54e;
          border-radius: 0 4px 4px 0;
        }
      }

      .user-item {
        cursor: pointer;
        &:hover {
          background: rgba(255, 255, 255, 0.08);
        }
      }
    }
  }

  .topbar {
    border-bottom: 1px solid #e2e7f2;
    :deep(.v-toolbar-title__placeholder) {
      font-size: 18px;
      font-weight: 600;
      color: #1c2942;
    }
  }
}
</style>
