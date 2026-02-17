export const requiredRule = (value?: string | null) => !!value?.trim() || 'This field is required.';

export const noHorribleRule = (value?: string | null) =>
  !value || !/\bhorrible\b/i.test(value) || 'Comments cannot contain the word "horrible".';

export const commentsRequiredWhenRatedRule = (rating?: number | null) => (value?: string | null) =>
  !rating || !!value?.trim() || 'Comments are required when rating is provided.';

export const pageSizeMaxRule = (value?: number | null) =>
  value == null || value <= 25 || 'Maximum page size is 25';

export const pageSizeMinRule = (value?: number | null) =>
  value == null || value >= 1 || 'Minimum page size is 1';
