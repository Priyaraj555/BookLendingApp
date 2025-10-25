using BookLendingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLendingApp.Services
{
    public class InMemoryBookService : IBookService
    {
        private Dictionary<string, Book> books = new Dictionary<string, Book>();

        public bool AddBook(Book book)
        {
            if (book == null || string.IsNullOrWhiteSpace(book.ISBN))
            {
                Console.WriteLine("Invalid book details.");
                return false;
            }

            if (books.ContainsKey(book.ISBN))
            {
                Console.WriteLine($"A book with ISBN '{book.ISBN}' already exists. Duplicate not allowed.");
                return false;
            }

            books[book.ISBN] = book;
            Console.WriteLine($"Book '{book.Title}' added successfully!");
            return true;
        }

        public Book GetBookByISBN(string isbn) => books.ContainsKey(isbn) ? books[isbn] : null;

        public List<Book> SearchByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                return new List<Book>();

            return books.Values
                        .Where(b => b.Authors.Any(a => a.Equals(author, StringComparison.OrdinalIgnoreCase)))
                        .ToList();
        }

        public List<Book> GetTopBooks(int topN)
        {
            return books.Values
                        .OrderByDescending(b => b.TimesBorrowed)
                        .Take(topN)
                        .ToList();
        }

        public List<Book> GetAllBooks() => new List<Book>(books.Values);
    }
}
