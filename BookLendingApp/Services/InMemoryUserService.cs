using BookLendingApp.Models;
using System;
using System.Collections.Generic;

namespace BookLendingApp.Services
{
    public class InMemoryUserService : IUserService
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private IBookService bookService;

        public InMemoryUserService(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public void AddUser(User user) => users[user.UserId] = user;

        public bool BorrowBook(string userId, string isbn)
        {
            if (!users.ContainsKey(userId))
            {
                Console.WriteLine("User not found.");
                return false;
            }

            var user = users[userId];
            var book = bookService.GetBookByISBN(isbn);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return false;
            }

            if (book.AvailableCopies <= 0)
            {
                Console.WriteLine("No copies available for borrowing.");
                return false;
            }

            // Reduce available copies
            book.AvailableCopies--;

            // Add borrow history
            user.BorrowHistory.Add(new BorrowHistory
            {
                BookISBN = isbn,
                BorrowedOn = DateTime.Now
            });

            // Increase times borrowed
            book.TimesBorrowed++;

            Console.WriteLine($"Book '{book.Title}' borrowed successfully by {user.Name}.");
            return true;
        }

        public bool ReturnBook(string userId, string isbn)
        {
            if (!users.ContainsKey(userId))
            {
                Console.WriteLine("User not found.");
                return false;
            }

            var user = users[userId];
            var book = bookService.GetBookByISBN(isbn);

            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return false;
            }

            // Find the borrow record that has not been returned yet
            var borrowRecord = user.BorrowHistory.Find(b => b.BookISBN == isbn && b.ReturnedOn == null);

            if (borrowRecord == null)
            {
                Console.WriteLine("This book was not borrowed by the user or already returned.");
                return false;
            }

            // Mark as returned
            borrowRecord.ReturnedOn = DateTime.Now;

            // Increase available copies
            book.AvailableCopies++;

            Console.WriteLine($"Book '{book.Title}' returned successfully by {user.Name}.");
            return true;
        }

        public List<BorrowHistory> GetBorrowHistory(string userId)
        {
            return users.ContainsKey(userId) ? users[userId].BorrowHistory : new List<BorrowHistory>();
        }
    }
}
