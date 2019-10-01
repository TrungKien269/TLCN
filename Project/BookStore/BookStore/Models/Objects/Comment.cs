using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Comment
    {
        public int UserId { get; set; }
        public string BookId { get; set; }
        public string Comment1 { get; set; }
        public DateTime DateTime { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }
}
