using System.Collections.Generic;
using BookLendingApp.Models;

namespace BookLendingApp.Services
{
    public interface IBookService
    {
        bool AddBook(Book book);
        Book GetBookByISBN(string isbn);
        List<Book> SearchByAuthor(string author);
        List<Book> GetTopBooks(int topN);
        List<Book> GetAllBooks(); 
    }
}
