﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IteratorsAndComparators
{
    public class Library : IEnumerable<Book>
    {
        private List<Book> books;

        public Library(params Book[] books)
        {
            this.books = books
                .OrderBy(x => x.Title)
                .ThenByDescending(x => x.Year)
                .ToList();

            //this.books = books
            //    .OrderBy(x => x.Year)
            //    .ThenBy(x => x.Title)
            //    .ToList();
        }

        public IEnumerator<Book> GetEnumerator()
        {

            LibraryIterator libraryIterator = new LibraryIterator(this.books.ToArray());

            return libraryIterator;

            //for (int i = 0; i < this.books.Count; i++)
            //{
            //    yield return this.books[i];
            //}
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private class LibraryIterator : IEnumerator<Book>
        {
            private List<Book> books;
            private int currentIndex;

            public LibraryIterator(params Book[] books)
            {
                this.books = books.ToList();
                this.currentIndex = -1;
            }

            public Book Current => this.books[this.currentIndex];

            object IEnumerator.Current => this.Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                bool isNext = this.currentIndex + 1 <= this.books.Count - 1;
                if (!isNext)
                {
                    return false;
                }

                this.currentIndex++;

                return true;
            }

            public void Reset()
            {
                this.currentIndex = -1;
            }
        }
    }
}