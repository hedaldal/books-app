export interface Book {
  id: string;
  title: string;
  author: string;
  isbn: string;
  coverImageUrl: string | null;
  rating: number | null;
  comments: string | null;
}

export interface BookListResponse {
  items: Book[];
  totalCount: number;
}

export type SortOrder = 'asc' | 'desc';

export interface GetBooksParams {
  page: number;
  pageSize: number;
  search?: string;
  sort: SortOrder;
}

export interface CreateBookPayload {
  title: string;
  author: string;
  isbn: string;
  coverImageUrl: string;
  rating: number | null;
  comments: string | null;
}

export interface UpdateBookPayload {
  rating: number | null;
  comments: string | null;
}
