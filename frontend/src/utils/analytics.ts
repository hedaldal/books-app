import { Book } from '@/types/book';

export const calculateAverageRating = (books: Book[]): number => {
  const ratings = books.map((book) => book.rating).filter((r) => r !== null && r > 0) as number[];
  if (ratings.length === 0) {
    return 0;
  }

  const sum = ratings.reduce((total, item) => total + item);
  return Number((sum / ratings.length).toFixed(2));
};
