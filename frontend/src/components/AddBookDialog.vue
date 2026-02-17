<template>
  <v-dialog v-model="open" max-width="640">
    <v-form ref="formRef">
      <v-card>
        <v-card-title class="text-h6 mt-2">Add Book</v-card-title>
        <v-card-text>
          <div class="d-flex flex-column ga-1">
            <v-text-field
              variant="outlined"
              density="comfortable"
              v-model.trim="form.title"
              label="Title"
              maxlength="200"
              :rules="[requiredRule]"
            />
            <v-text-field
              variant="outlined"
              density="comfortable"
              v-model.trim="form.author"
              label="Author"
              maxlength="100"
              :rules="[requiredRule]"
            />
            <v-text-field
              variant="outlined"
              density="comfortable"
              v-model="form.isbn"
              label="ISBN"
              maxlength="32"
              :rules="[requiredRule]"
            />
            <v-text-field
              variant="outlined"
              density="comfortable"
              v-model.trim="form.coverImageUrl"
              maxlength="2048"
              label="Cover Image URL"
            />

            <div class="text-grey-darken-1 mt-n2">Rating</div>
            <BookRating v-model:rating="form.rating" class="mb-2" :size="32" />

            <v-textarea
              v-model.trim="form.comments"
              label="Comments"
              rows="2"
              variant="outlined"
              density="comfortable"
              maxlength="1000"
              :rules="[commentsRequiredWhenRatedRule(form.rating), noHorribleRule]"
            />
          </div>
        </v-card-text>
        <v-card-actions class="mx-3 mt-n3 mb-2">
          <v-spacer />
          <v-btn variant="text" @click="open = false">Cancel</v-btn>
          <v-btn color="primary" variant="flat" prepend-icon="mdi-content-save" @click="submit"
            >Save</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script setup lang="ts">
import type { CreateBookPayload } from '@/types/book';
import { commentsRequiredWhenRatedRule, noHorribleRule, requiredRule } from '@/utils/validation';
import { computed, reactive, ref, watch } from 'vue';
import BookRating from './BookRating.vue';

const open = defineModel({ type: Boolean, required: true });
const emit = defineEmits<{ submit: [payload: CreateBookPayload] }>();

const form = reactive({
  title: '',
  author: '',
  isbn: '',
  coverImageUrl: '',
  rating: null as number | null,
  comments: '',
});
const formRef = ref<any>(null);

const hasRating = computed(
  () => typeof form.rating === 'number' && form.rating > 0 && form.rating <= 5
);

watch(
  () => open.value,
  (value) => {
    if (!value) {
      return;
    }

    form.title = '';
    form.author = '';
    form.isbn = '';
    form.coverImageUrl = '';
    form.rating = null;
    form.comments = '';
  }
);

async function submit() {
  const result = await formRef.value?.validate();
  if (!result?.valid) return;

  emit('submit', {
    title: form.title,
    author: form.author,
    isbn: form.isbn,
    coverImageUrl: form.coverImageUrl,
    rating: hasRating.value ? Number(form.rating) : null,
    comments: form.comments,
  });
}
</script>
