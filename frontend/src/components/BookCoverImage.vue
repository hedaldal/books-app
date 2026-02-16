<template>
  <div>
    <v-img
      v-if="coverImageUrl"
      :src="coverImageUrl"
      cover
      :width="width"
      :height="height"
      class="rounded-lg"
    />
    <div v-else class="no-cover" :style="`width: ${width}px; height: ${height}px`">
      <v-icon color="white">mdi-book</v-icon>
    </div>
  </div>
</template>

<script setup lang="ts">
interface Props {
  coverImageUrl: string | null;
  width?: number;
  height?: number;
}
withDefaults(defineProps<Props>(), {
  width: 63,
  height: 88,
});
</script>
<style lang="scss" scoped>
.no-cover {
  border-radius: 8px;
  background: linear-gradient(135deg, #5c6bc0, #7e57c2);
  display: flex;
  align-items: center;
  justify-content: center;
}
</style>
