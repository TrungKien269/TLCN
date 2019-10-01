using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class AuthorBook
    {
        public string BookId { get; set; }
        public int AuthorId { get; set; }

        public Author Author { get; set; }
        public Book Book { get; set; }
    }
}
