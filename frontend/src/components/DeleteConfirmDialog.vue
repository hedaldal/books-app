<template>
  <v-dialog v-model="open" max-width="420">
    <v-card rounded="lg" class="pa-2 pt-4">
      <v-card-title class="d-flex align-center ga-2 text-h6">
        <v-icon color="error">mdi-alert-circle-outline</v-icon>
        Delete book
      </v-card-title>

      <v-card-text class="pt-2">
        <div>
          Are you sure you want to delete <strong>{{ title }}</strong
          >? This action cannot be undone.
        </div>
      </v-card-text>

      <v-card-actions class="pt-0 pb-4 px-4">
        <v-spacer />
        <v-btn variant="text" @click="open = false"> Cancel </v-btn>
        <v-btn color="error" variant="flat" prepend-icon="mdi-delete" @click="emit('confirm')">
          Delete
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
const open = defineModel({ type: Boolean, required: true });
defineProps<{ title?: string }>();
const emit = defineEmits(['confirm']);
</script>
