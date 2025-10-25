using System.Collections.Generic;
using BookLendingApp.Models;

namespace BookLendingApp.Services
{
    public interface IUserService
    {
        void AddUser(User user);
        bool BorrowBook(string userId, string isbn);
        bool ReturnBook(string userId, string isbn);
        List<BorrowHistory> GetBorrowHistory(string userId);
    }
}
