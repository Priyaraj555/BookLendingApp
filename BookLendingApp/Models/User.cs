using System.Collections.Generic;

namespace BookLendingApp.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<BorrowHistory> BorrowHistory { get; set; } = new List<BorrowHistory>();
    }
}