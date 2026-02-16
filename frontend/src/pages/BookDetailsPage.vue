<template>
  <section>
    <v-btn to="/books" variant="text" prepend-icon="mdi-arrow-left" class="mb-4 text-none">
      Back to My Books
    </v-btn>
    <v-alert v-if="error" type="error" variant="tonal">{{ error }}</v-alert>

    <v-card class="pa-4" border>
      <v-row class="align-start" dense>
        <v-col cols="12" md="4" class="d-flex justify-center justify-md-start">
          <BookCoverImage
            :coverImageUrl="book.coverImageUrl"
            :width="220"
            :height="320"
            class="mb-2"
          />
        </v-col>

        <v-col cols="12" md="8">
          <h2 class="text-h5 mb-2">{{ book.title }}</h2>
          <p class="text-medium-emphasis mb-4">{{ book.author }}</p>

          <div><strong>ISBN:</strong> {{ book.isbn }}</div>
          <div class="d-flex align-center ga-2 my-2">
            <strong>Rating:</strong>
            <BookRating :rating="book.rating" readonly />
          </div>
          <div><strong>Comments:</strong> {{ book.comments }}</div>
        </v-col>
      </v-row>
    </v-card>
  </section>
</template>
<script setup lang="ts">
import { extractApiError, getBook } from '@/api/booksApi';
import BookCoverImage from '@/components/BookCoverImage.vue';
import BookRating from '@/components/BookRating.vue';
import type { Book } from '@/types/book';
import { computed, onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const book = ref<Book>({
  id: '',
  title: '',
  author: '',
  isbn: '',
  coverImageUrl: null,
  rating: null,
  comments: null,
});
const error = ref('');

const bookId = computed(() => {
  const id = route.params.id;
  if (Array.isArray(id)) {
    return id[0] || '';
  }

  return id;
});

const loadBook = async () => {
  if (!bookId.value) {
    error.value = 'Book id is missing.';
    return;
  }

  try {
    book.value = await getBook(bookId.value);
  } catch (errorValue: unknown) {
    error.value = extractApiError(errorValue);
  }
};

onMounted(() => {
  void loadBook();
});
</script>

<style lang="scss" scoped></style>
