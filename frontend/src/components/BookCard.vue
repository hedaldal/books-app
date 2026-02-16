<template>
  <v-card class="book-card mb-2" variant="flat" border>
    <v-row align="center">
      <v-col cols="12" sm="6">
        <div class="d-flex align-center">
          <div class="d-flex align-stretch">
            <BookCoverImage :coverImageUrl="book.coverImageUrl" />
            <div class="d-flex flex-column ga-2 ml-4">
              <div class="text-subtitle-1 font-weight-bold">{{ book.title }}</div>
              <div class="text-body-2 text-medium-emphasis">{{ book.author }}</div>
              <div class="text-caption text-medium-emphasis">
                ISBN: {{ book.isbn }}
                <v-chip v-if="hasNotes(book)" size="x-small" color="green" variant="tonal">
                  Has notes
                </v-chip>
              </div>
            </div>
          </div>
        </div>
      </v-col>
      <v-col cols="12" sm="6" class="pt-0">
        <div class="book-actions d-flex align-center ga-2 justify-space-between justify-sm-end">
          <BookRating :rating="book.rating" readonly />
          <div class="d-flex align-center ga-2">
            <v-btn variant="tonal" size="small" class="action-btn" @click="emit('openEdit', book)">
              <v-icon color="grey-darken-3" size="18">mdi-pencil</v-icon>
            </v-btn>

            <v-btn
              variant="tonal"
              size="small"
              class="action-btn"
              @click="router.push(`/books/${book.id}`)"
            >
              <v-icon color="grey-darken-3" size="18">mdi-eye</v-icon>
            </v-btn>

            <v-btn
              variant="tonal"
              size="small"
              class="action-btn"
              @click="emit('openDelete', book)"
            >
              <v-icon color="grey-darken-3" size="18">mdi-delete</v-icon>
            </v-btn>
          </div>
        </div>
      </v-col>
    </v-row>
  </v-card>
</template>
<script lang="ts" setup>
import { Book } from '@/types/book';
import { useRouter } from 'vue-router';
import BookCoverImage from './BookCoverImage.vue';
import BookRating from './BookRating.vue';

interface Props {
  book: Book;
}

defineProps<Props>();
const emit = defineEmits(['openDelete', 'openEdit']);

const router = useRouter();

const hasNotes = (book: Book): boolean => {
  return !!book.comments;
};
</script>
<style lang="scss" scoped>
.book-card {
  border-radius: 12px;
  background: #fff;
  padding: 12px;

  .book-actions {
    .action-btn {
      color: grey;
      min-width: 30px;
      padding: 0;
      border-radius: 8px;
    }
  }
}
</style>
