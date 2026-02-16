import type {
  Book,
  BookListResponse,
  CreateBookPayload,
  GetBooksParams,
  UpdateBookPayload,
} from '@/types/book';
import axios from 'axios';

const client = axios.create({
  baseURL: `${import.meta.env.VITE_API_BASE_URL}/api/books`,
});

export async function getBooks({ page, pageSize, search, sort }: GetBooksParams) {
  const { data } = await client.get<BookListResponse>('', {
    params: { page, pageSize, search: search || undefined, sort },
  });
  return data;
}

export async function getBook(id: string) {
  const { data } = await client.get<Book>(`/${id}`);
  return data;
}

export async function createBook(payload: CreateBookPayload) {
  const { data } = await client.post<Book>('', payload);
  return data;
}

export async function updateBook(id: string, payload: UpdateBookPayload) {
  const { data } = await client.patch<Book>(`/${id}`, payload);
  return data;
}

export async function deleteBook(id: string) {
  await client.delete(`/${id}`);
}

export function extractApiError(error: unknown): string {
  if (
    typeof error === 'object' &&
    error !== null &&
    'response' in error &&
    typeof error.response === 'object' &&
    error.response !== null &&
    'data' in error.response
  ) {
    const data = (error.response as { data?: unknown }).data;
    if (
      typeof data === 'object' &&
      data !== null &&
      'errors' in data &&
      typeof data.errors === 'object' &&
      data.errors !== null
    ) {
      return Object.values(data.errors as Record<string, string[]>)
        .flat()
        .join(' ');
    }

    if (
      typeof data === 'object' &&
      data !== null &&
      'detail' in data &&
      typeof data.detail === 'string'
    ) {
      return data.detail;
    }
  }

  return 'Request failed.';
}
