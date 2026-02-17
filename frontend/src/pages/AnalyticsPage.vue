<template>
  <section>
    <v-alert v-if="error" type="error" variant="tonal" class="mb-4">{{ error }}</v-alert>
    <v-row>
      <v-col cols="12" md="4">
        <v-card border>
          <v-card-title>Total Books</v-card-title>
          <v-card-text class="text-h4">{{ totalBooks }}</v-card-text>
        </v-card>
      </v-col>

      <v-col cols="12" md="4">
        <v-card border>
          <v-card-title>Average Rating</v-card-title>
          <v-card-text class="text-h4">{{ averageRating }}</v-card-text>
        </v-card>
      </v-col>

      <v-col cols="12" md="4">
        <v-card border>
          <v-card-title>Rated Books</v-card-title>
          <v-card-text class="text-h4">{{ ratedBooks }}</v-card-text>
        </v-card>
      </v-col>
    </v-row>
  </section>
</template>
<script setup lang="ts">
import { extractApiError, getBooks } from '@/api/booksApi';
import type { Book } from '@/types/book';
import { calculateAverageRating } from '@/utils/analytics';
import { computed, onMounted, ref } from 'vue';

const error = ref('');
const books = ref<Book[]>([]);

const totalBooks = computed(() => books.value.length);
const ratedBooks = computed(
  () => books.value.filter((book) => book.rating !== null && book.rating > 0).length
);
const averageRating = computed(() => calculateAverageRating(books.value));

async function loadAnalytics() {
  error.value = '';

  try {
    const data = await getBooks({ page: 1, pageSize: 25, sort: 'asc', search: '' });
    books.value = data.items;
  } catch (errorValue: unknown) {
    error.value = extractApiError(errorValue);
  }
}

onMounted(() => {
  void loadAnalytics();
});
</script>
