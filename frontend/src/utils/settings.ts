const PAGE_SIZE_KEY = 'books.pageSize';
const DISPLAY_NAME_KEY = 'displayName';

export const getDefaultPageSize = (): number => {
  const value = Number(localStorage.getItem(PAGE_SIZE_KEY) || 10);
  if (Number.isNaN(value) || value < 1 || value > 25) {
    return 10;
  }

  return value;
};

export const setDefaultPageSize = (size: number) => {
  localStorage.setItem(PAGE_SIZE_KEY, String(size));
};

export const getDisplayName = (): string => {
  return localStorage.getItem(DISPLAY_NAME_KEY) || 'Anonymous';
};

export const setDisplayName = (name: string) => {
  localStorage.setItem(DISPLAY_NAME_KEY, name);
};
