using System;

namespace BookLendingApp.Models
{
    public class BorrowHistory
    {
        public string BookISBN { get; set; }
        public DateTime BorrowedOn { get; set; }
        public DateTime? ReturnedOn { get; set; } = null;
    }
}