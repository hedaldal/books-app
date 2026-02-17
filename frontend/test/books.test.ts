import { Book } from '@/types/book';
import { calculateAverageRating } from '@/utils/analytics';
import { noHorribleRule, requiredRule } from '@/utils/validation';
import { describe, expect, it } from 'vitest';

describe('validation rules', () => {
  it('requiredRule should fail on empty value', () => {
    expect(requiredRule('')).toBe('This field is required.');
  });

  it('requiredRule should pass on valid value', () => {
    expect(requiredRule('Dune')).toBe(true);
  });

  it('noHorribleRule should fail if contains forbidden word', () => {
    expect(noHorribleRule('This is horrible')).toBe('Comments cannot contain the word "horrible".');
  });

  it('noHorribleRule should fail if contains forbidden word', () => {
    expect(noHorribleRule('This is HORRIBLE.')).toBe(
      'Comments cannot contain the word "horrible".'
    );
  });

  it('noHorribleRule should pass for valid comment', () => {
    expect(noHorribleRule('Great book')).toBe(true);
  });
});

describe('analytics helpers', () => {
  it('calculates average rating', () => {
    const books: Book[] = [
      { title: 'Book 1', author: 'Author A', rating: 0 } as Book,
      { title: 'Book 2', author: 'Author B', rating: 5 } as Book,
      { title: 'Book 2', author: 'Author B', rating: 4 } as Book,
      { title: 'Book 2', author: 'Author B', rating: null } as Book,
    ];

    expect(calculateAverageRating(books)).toBe(4.5);
  });
});
