using System.Collections.Generic;

namespace BookLendingApp.Models
{
    public class Book
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public List<string> Authors { get; set; } = new List<string>();
        public List<string> Genres { get; set; } = new List<string>();
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int TimesBorrowed { get; set; } = 0;
    }
}
