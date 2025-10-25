# Book Lending & Recommendation Service

A .NET 6+ console application that manages books, users, borrowing/returning, and recommends books based on borrowing history.

✅ Features

-> Add and list books (with ISBN uniqueness).

-> Add users.

-> Borrow and return books (with history tracking).

-> Recommend books (genre-based + fallback random).

-> Search by author.

-> View top N most popular books.


🏗️ Design Decisions

1.Clean Architecture

Core models (Book, User, BorrowHistory) separated from services.

Service interfaces (IBookService, IUserService, IRecommendationService) allow swapping implementations (e.g., switch from in-memory to database).

2.In-Memory Storage (Trade-off)

Chosen for simplicity and fast prototyping.

Data resets when program exits.

Could be swapped for SQLite / EF Core / JSON file persistence.

3.Duplicate ISBN Handling

Design allows two options:

Reject duplicates (strict uniqueness).

Increase total copies (realistic library).

Current default: Option 1 (reject duplicates).

4.Recommendation Algorithm

Counts genres in user’s borrow history.

Prioritizes most frequently borrowed genres.

Excludes already borrowed books.

Fills gaps with random books.

Trade-off: Works well for small datasets, but may need ML/collaborative filtering at scale.

5.Console UI

Menu-driven, simple to use.

Trade-off: Not scalable for thousands of books/users. A web API + frontend would be better for real-world usage.

🚀 If Given More Time

Add persistent storage (SQLite/EF Core).

Add due dates + overdue tracking.

Add unit tests for borrow/return/recommendation.

Support multiple concurrent users.

Add REST API with ASP.NET Core for scaling to web/mobile apps.

Implement role-based access (e.g., librarian vs user).

📂 Folder Structure
BookLendingApp/
│
├── Models/
│   ├── Book.cs
│   ├── User.cs
│   └── BorrowHistory.cs
│
├── Services/
│   ├── IBookService.cs
│   ├── IUserService.cs
│   ├── IRecommendationService.cs
│   ├── InMemoryBookService.cs
│   ├── InMemoryUserService.cs
│   └── InMemoryRecommendationService.cs
│
├── Program.cs        // Console menu
└── README.md

🛠️ Build & Run
Prerequisites

Visual Studio 2022 (or VS Code with C# extension).

.NET 6 or later installed.

Steps

1.Clone or create project folder

git clone <your-repo-url>
cd BookLendingApp


2.Build project

dotnet build


3.Run project

dotnet run


4.Use Console Menu

1. Add Book
2. List Books
3. Add User
4. Borrow Book
5. Return Book
6. Recommend Books
7. Search Books by Author
8. Top N Popular Books
9. Exit

👨‍💻 Author Notes

This project is designed as a learning/demo app.
For production-level systems:

Replace in-memory storage with EF Core + SQL Server.

Add logging, exception handling, and validation.

Provide a REST API layer with Swagger for testing.