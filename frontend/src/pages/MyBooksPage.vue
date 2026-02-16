<template>
  <section>
    <div class="toolbar mb-5">
      <v-text-field
        v-model="search"
        label="Search title or author"
        variant="outlined"
        density="compact"
        hide-details
        prepend-inner-icon="mdi-magnify"
        class="search-input"
        @keyup.enter="applyFilters"
      />

      <v-select
        v-model="sort"
        :items="sortOptions"
        label="Sort"
        variant="outlined"
        density="compact"
        hide-details
        class="sort-select"
        @update:model-value="applyFilters"
      />

      <v-btn color="primary" variant="flat" @click="applyFilters">Search</v-btn>
      <v-btn color="primary" variant="tonal" @click="showAddDialog = true">Add Book</v-btn>
    </div>

    <v-alert v-if="error" type="error" variant="tonal" class="mb-4">{{ error }}</v-alert>

    <div class="book-list">
      <BookCard
        v-for="book in books"
        :key="book.id"
        :book="book"
        @open-delete="openDelete"
        @open-edit="openEdit"
      />

      <v-card
        v-if="books.length === 0"
        variant="tonal"
        class="pa-6 text-center text-medium-emphasis"
      >
        No books found.
      </v-card>
    </div>

    <div class="d-flex justify-space-between align-center mt-6 flex-wrap ga-4">
      <div class="text-medium-emphasis">{{ totalCount }} books</div>
      <v-pagination
        v-model="page"
        :length="totalPages"
        :total-visible="7"
        @update:model-value="loadBooks"
      />
    </div>

    <AddBookDialog v-model="showAddDialog" @submit="handleCreate" />
    <EditBookDialog v-model="showEditDialog" :book="selectedBook" @submit="handleUpdate" />
    <DeleteConfirmDialog
      v-model="showDeleteDialog"
      :title="selectedBook?.title"
      @confirm="handleDelete"
    />
  </section>
</template>
<script setup lang="ts">
import { createBook, deleteBook, extractApiError, getBooks, updateBook } from '@/api/booksApi';
import AddBookDialog from '@/components/AddBookDialog.vue';
import BookCard from '@/components/BookCard.vue';
import DeleteConfirmDialog from '@/components/DeleteConfirmDialog.vue';
import EditBookDialog from '@/components/EditBookDialog.vue';
import type { Book, CreateBookPayload, SortOrder, UpdateBookPayload } from '@/types/book';
import { getDefaultPageSize } from '@/utils/settings';
import { computed, onMounted, ref } from 'vue';

const books = ref<Book[]>([]);

const error = ref('');

const page = ref(1);
const pageSize = ref(getDefaultPageSize());
const totalCount = ref(0);
const search = ref('');
const sort = ref<SortOrder>('asc');

const showAddDialog = ref(false);
const showEditDialog = ref(false);
const showDeleteDialog = ref(false);

const selectedBook = ref<Book | null>(null);

const totalPages = computed(() => {
  const pages = Math.ceil(totalCount.value / pageSize.value);
  return pages > 0 ? pages : 1;
});

const sortOptions: Array<{ title: string; value: SortOrder }> = [
  { title: 'Title A-Z', value: 'asc' },
  { title: 'Title Z-A', value: 'desc' },
];

const loadBooks = async () => {
  error.value = '';

  try {
    const data = await getBooks({
      page: page.value,
      pageSize: pageSize.value,
      sort: sort.value,
      search: search.value,
    });
    books.value = data.items;
    totalCount.value = data.totalCount;
  } catch (errorValue: unknown) {
    error.value = extractApiError(errorValue);
  }
};

const applyFilters = () => {
  page.value = 1;
  void loadBooks();
};

const openEdit = (book: Book) => {
  selectedBook.value = book;
  showEditDialog.value = true;
};

const openDelete = (book: Book) => {
  selectedBook.value = book;
  showDeleteDialog.value = true;
};

const handleCreate = async (payload: CreateBookPayload) => {
  error.value = '';
  try {
    await createBook(payload);
    showAddDialog.value = false;
    page.value = 1;
    await loadBooks();
  } catch (errorValue: unknown) {
    error.value = extractApiError(errorValue);
  }
};

const handleUpdate = async (payload: UpdateBookPayload) => {
  if (!selectedBook.value) {
    return;
  }

  error.value = '';
  try {
    await updateBook(selectedBook.value.id, payload);
    showEditDialog.value = false;
    await loadBooks();
  } catch (errorValue: unknown) {
    error.value = extractApiError(errorValue);
  }
};

const handleDelete = async () => {
  if (!selectedBook.value) {
    return;
  }

  error.value = '';
  try {
    await deleteBook(selectedBook.value.id);
    showDeleteDialog.value = false;
    await loadBooks();
  } catch (errorValue: unknown) {
    error.value = extractApiError(errorValue);
  }
};

onMounted(() => {
  void loadBooks();
});
</script>

<style lang="scss" scoped>
.toolbar {
  display: flex;
  align-items: center;
  gap: 12px;
  flex-wrap: wrap;
}

.search-input {
  min-width: 260px;
  max-width: 380px;
}

.sort-select {
  width: 180px;
}
</style>
