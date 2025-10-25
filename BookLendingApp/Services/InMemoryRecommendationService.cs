using System;
using System.Collections.Generic;
using System.Linq;
using BookLendingApp.Models;

namespace BookLendingApp.Services
{
    public class InMemoryRecommendationService : IRecommendationService
    {
        private IBookService bookService;
        private IUserService userService;

        public InMemoryRecommendationService(IBookService bookService, IUserService userService)
        {
            this.bookService = bookService;
            this.userService = userService;
        }

        public List<Book> RecommendBooksForUser(string userId)
        {
            var allBooks = bookService.GetAllBooks().Where(b => b.AvailableCopies > 0).ToList();

            if (!allBooks.Any())
                return new List<Book>();

            var userBorrowHistory = userService.GetBorrowHistory(userId);

            // If no borrowing history, return 3 random available books
            if (userBorrowHistory == null || userBorrowHistory.Count == 0)
            {
                return allBooks.OrderBy(b => Guid.NewGuid()).Take(3).ToList();
            }

            // Count genres from borrowed books
            var borrowedBooks = userBorrowHistory.Select(h => bookService.GetBookByISBN(h.BookISBN)).Where(b => b != null);
            var genreFrequency = new Dictionary<string, int>();

            foreach (var book in borrowedBooks)
            {
                foreach (var genre in book.Genres)
                {
                    if (genreFrequency.ContainsKey(genre))
                        genreFrequency[genre]++;
                    else
                        genreFrequency[genre] = 1;
                }
            }

            // Sort genres by frequency descending
            var sortedGenres = genreFrequency.OrderByDescending(g => g.Value).Select(g => g.Key).ToList();

            var recommendedBooks = new List<Book>();

            // Recommend books by preferred genres
            foreach (var genre in sortedGenres)
            {
                var booksInGenre = allBooks
                    .Where(b => b.Genres.Contains(genre) && !userBorrowHistory.Any(h => h.BookISBN == b.ISBN))
                    .ToList();

                foreach (var b in booksInGenre)
                {
                    if (recommendedBooks.Count < 3)
                        recommendedBooks.Add(b);
                    else
                        break;
                }

                if (recommendedBooks.Count >= 3)
                    break;
            }

            // If less than 3, fill randomly with remaining available books
            if (recommendedBooks.Count < 3)
            {
                var remainingBooks = allBooks.Except(recommendedBooks)
                                             .Where(b => !userBorrowHistory.Any(h => h.BookISBN == b.ISBN))
                                             .OrderBy(b => Guid.NewGuid())
                                             .Take(3 - recommendedBooks.Count)
                                             .ToList();
                recommendedBooks.AddRange(remainingBooks);
            }

            return recommendedBooks;
        }
    }
}
