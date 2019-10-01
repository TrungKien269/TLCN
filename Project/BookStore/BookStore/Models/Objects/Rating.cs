using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Rating
    {
        public int UserId { get; set; }
        public string BookId { get; set; }
        public int Point { get; set; }
        public DateTime DateTime { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}
