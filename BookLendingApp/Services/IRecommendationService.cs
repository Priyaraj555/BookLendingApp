using System.Collections.Generic;
using BookLendingApp.Models;

namespace BookLendingApp.Services
{
    public interface IRecommendationService
    {
        List<Book> RecommendBooksForUser(string userId);
    }
}
