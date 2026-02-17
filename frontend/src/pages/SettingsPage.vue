<template>
  <section>
    <v-alert v-if="message" type="success" variant="tonal" class="mb-4">{{ message }}</v-alert>
    <v-form v-model="isValid" ref="formRef">
      <v-card max-width="520" border>
        <v-card-title class="mb-2">Display</v-card-title>
        <v-card-text>
          <v-text-field
            v-model.trim="displayName"
            label="Profile name"
            variant="outlined"
            density="comfortable"
            class="mb-2"
            :rules="[requiredRule]"
          />

          <v-text-field
            v-model.number="defaultPageSize"
            type="number"
            min="1"
            max="25"
            label="Default page size for My Books"
            variant="outlined"
            density="comfortable"
            :rules="[pageSizeMinRule, pageSizeMaxRule]"
          />
        </v-card-text>
        <v-card-actions>
          <v-spacer />
          <v-btn color="primary" :disabled="!isValid" @click="saveSettings">Save</v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </section>
</template>
<script setup lang="ts">
import { useProfile } from '@/composables/useProfile';
import { getDefaultPageSize, getDisplayName, setDefaultPageSize } from '@/utils/settings';
import { pageSizeMaxRule, pageSizeMinRule, requiredRule } from '@/utils/validation';
import { ref } from 'vue';

const { updateProfileName } = useProfile();
const defaultPageSize = ref(getDefaultPageSize());
const displayName = ref(getDisplayName());

const message = ref('');
const isValid = ref(false);

const saveSettings = () => {
  updateProfileName(displayName.value);
  setDefaultPageSize(defaultPageSize.value);
  message.value = 'Settings saved.';
};
</script>
