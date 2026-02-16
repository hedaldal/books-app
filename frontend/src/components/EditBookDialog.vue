<template>
  <v-dialog v-model="open" max-width="420">
    <v-card rounded="lg" class="pa-2 pt-4">
      <v-card-title class="d-flex align-center ga-2 text-h6"> Edit book </v-card-title>

      <v-card-text class="d-flex flex-column ga-3">
        <BookRating v-model:rating="rating" :size="32" />
        <v-textarea
          v-model.trim="comments"
          label="Comments"
          rows="2"
          variant="outlined"
          density="comfortable"
          class="mt-2"
          :error="commentsRequired"
          :error-messages="commentsRequired ? 'Comments are required when rating is provided.' : ''"
        />
      </v-card-text>

      <v-card-actions class="pt-0 pb-4 px-4">
        <v-spacer />
        <v-btn variant="text" @click="open = false"> Cancel </v-btn>
        <v-btn
          color="primary"
          variant="flat"
          :disabled="!isValid"
          prepend-icon="mdi-content-save"
          @click="submit"
          >Update</v-btn
        >
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import type { Book, UpdateBookPayload } from '@/types/book';
import { computed, ref, watch } from 'vue';
import BookRating from './BookRating.vue';

const open = defineModel({ type: Boolean, required: true });
const props = defineProps<{ book: Book | null }>();
const emit = defineEmits<{ submit: [payload: UpdateBookPayload] }>();

const rating = ref<number>(0);
const comments = ref<string>('');

watch(
  () => props.book,
  (book) => {
    rating.value = book?.rating ?? 0;
    comments.value = book?.comments ?? '';
  },
  { immediate: true }
);

const commentsRequired = computed(() => rating.value > 0 && !comments.value);
const isValid = computed(() => !commentsRequired.value);

const submit = () => {
  if (!isValid.value) {
    return;
  }

  emit('submit', {
    rating: rating.value === 0 ? null : rating.value,
    comments: comments.value,
  });
};
</script>
