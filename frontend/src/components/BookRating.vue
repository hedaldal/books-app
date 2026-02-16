<template>
  <div class="d-flex align-center ga-2">
    <v-rating
      hover
      :readonly="readonly"
      :size="size"
      clearable
      :length="5"
      :model-value="ratingLocal"
      @update:modelValue="onRatingUpdate"
      color="orange-lighten-1"
      class="rating-stars mb-1"
    />

    <div class="text-caption text-medium-emphasis">{{ rating ?? 0 }}/5</div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue';

const rating = defineModel<number | null>('rating', { required: true });
const { readonly = false, size = 20 } = defineProps<{
  readonly?: boolean;
  size?: number;
}>();

const ratingLocal = computed(() => rating.value ?? 0);

function onRatingUpdate(val: string | number) {
  rating.value = +val;
}
</script>

<style lang="scss" scoped>
.rating-stars {
  :deep(.v-rating__item) {
    label {
      font-size: 14px;
    }
  }
}
</style>
