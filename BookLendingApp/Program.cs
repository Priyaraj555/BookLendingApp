using BookLendingApp.Models;
using BookLendingApp.Services;
using System;
using System.Collections.Generic;

namespace BookLendingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IBookService bookService = new InMemoryBookService();
            IUserService userService = new InMemoryUserService(bookService);
            IRecommendationService recommendationService = new InMemoryRecommendationService(bookService, userService);

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n=== Book Lending Service ===");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. List Books");
                Console.WriteLine("3. Add User");
                Console.WriteLine("4. Borrow Book");
                Console.WriteLine("5. Return Book");
                Console.WriteLine("6. Recommend Books");
                Console.WriteLine("7. Search Books by Author");
                Console.WriteLine("8. Top N Popular Books");
                Console.WriteLine("9. Exit");
                Console.Write("Choose option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": // Add Book
                        Console.Write("Enter ISBN: ");
                        string isbn = Console.ReadLine();

                        Console.Write("Enter Title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter Authors (comma-separated): ");
                        string authorsInput = Console.ReadLine();
                        List<string> authors = new List<string>();
                        if (!string.IsNullOrEmpty(authorsInput))
                            authors = new List<string>(authorsInput.Split(',', (char)StringSplitOptions.RemoveEmptyEntries));

                        Console.Write("Enter Genres (comma-separated): ");
                        string genresInput = Console.ReadLine();
                        List<string> genres = new List<string>();
                        if (!string.IsNullOrEmpty(genresInput))
                            genres = new List<string>(genresInput.Split(',', (char)StringSplitOptions.RemoveEmptyEntries));

                        Console.Write("Enter Total Copies: ");
                        bool isNumber = int.TryParse(Console.ReadLine(), out int totalCopies);
                        if (!isNumber || totalCopies <= 0)
                        {
                            Console.WriteLine("Invalid number of copies. Operation cancelled.");
                            break;
                        }

                        Book newBook = new Book
                        {
                            ISBN = isbn,
                            Title = title,
                            Authors = authors,
                            Genres = genres,
                            TotalCopies = totalCopies,
                            AvailableCopies = totalCopies
                        };

                        bookService.AddBook(newBook);
                        //Console.WriteLine($"Book '{title}' added successfully!");
                        break;

                    case "2": // List Books
                        List<Book> allBooks = bookService.GetAllBooks();
                        if (allBooks.Count == 0)
                        {
                            Console.WriteLine("No books available.");
                            break;
                        }

                        Console.WriteLine("\n=== All Books ===");
                        foreach (var book in allBooks)
                        {
                            Console.WriteLine($"ISBN: {book.ISBN}, Title: {book.Title}, Authors: {string.Join(", ", book.Authors)}, Genres: {string.Join(", ", book.Genres)}, Available: {book.AvailableCopies}/{book.TotalCopies}");
                        }
                        break;

                    case "3": // Add User (placeholder)
                        Console.Write("Enter User ID: ");
                        string userId = Console.ReadLine();

                        Console.Write("Enter Name: ");
                        string userName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(userName))
                        {
                            Console.WriteLine("Invalid input. User not added.");
                            break;
                        }

                        User newUser = new User
                        {
                            UserId = userId,
                            Name = userName
                        };

                        userService.AddUser(newUser);
                        Console.WriteLine($"User '{userName}' added successfully!");
                        break;

                    case "4": // Borrow Book (placeholder)
                        Console.Write("Enter User ID: ");
                        string borrowUserId = Console.ReadLine();

                        Console.Write("Enter ISBN of Book to Borrow: ");
                        string borrowISBN = Console.ReadLine();

                        userService.BorrowBook(borrowUserId, borrowISBN);
                        break;

                    case "5": // Return Book (placeholder)
                        Console.Write("Enter User ID: ");
                        string returnUserId = Console.ReadLine();

                        Console.Write("Enter ISBN of Book to Return: ");
                        string returnISBN = Console.ReadLine();

                        userService.ReturnBook(returnUserId, returnISBN);
                        break;

                    case "6": // Recommend Books (placeholder)
                        Console.Write("Enter User ID: ");
                        string recUserId = Console.ReadLine();

                        var recommendations = recommendationService.RecommendBooksForUser(recUserId);

                        if (recommendations.Count == 0)
                        {
                            Console.WriteLine("No recommendations available at the moment.");
                        }
                        else
                        {
                            Console.WriteLine("\n=== Recommended Books ===");
                            foreach (var book in recommendations)
                            {
                                Console.WriteLine($"Title: {book.Title}, Authors: {string.Join(", ", book.Authors)}, Genres: {string.Join(", ", book.Genres)}, Available: {book.AvailableCopies}/{book.TotalCopies}");
                            }
                        }
                        break;
                    case "7": // Search by Author
                        Console.Write("Enter Author Name: ");
                        string authorName = Console.ReadLine();

                        var authorBooks = bookService.SearchByAuthor(authorName);
                        if (authorBooks.Count == 0)
                        {
                            Console.WriteLine("No books found for this author.");
                        }
                        else
                        {
                            Console.WriteLine($"\nBooks by '{authorName}':");
                            foreach (var book in authorBooks)
                            {
                                Console.WriteLine($"Title: {book.Title}, ISBN: {book.ISBN}, Available: {book.AvailableCopies}/{book.TotalCopies}");
                            }
                        }
                        break;

                    case "8": // Top N Popular Books
                        Console.Write("Enter N (number of top books): ");
                        bool isValid = int.TryParse(Console.ReadLine(), out int topN);
                        if (!isValid || topN <= 0)
                        {
                            Console.WriteLine("Invalid number.");
                            break;
                        }

                        var topBooks = bookService.GetTopBooks(topN);
                        if (topBooks.Count == 0)
                        {
                            Console.WriteLine("No books available.");
                        }
                        else
                        {
                            Console.WriteLine($"\nTop {topBooks.Count} Popular Books:");
                            foreach (var book in topBooks)
                            {
                                Console.WriteLine($"Title: {book.Title}, Times Borrowed: {book.TimesBorrowed}, Available: {book.AvailableCopies}/{book.TotalCopies}");
                            }
                        }
                        break;

                    case "9": // Exit
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

            }

            Console.WriteLine("Exiting app...");
        }
    }
}
