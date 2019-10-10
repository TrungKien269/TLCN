using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Author
    {
        public Author()
        {
            AuthorBook = new List<AuthorBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<AuthorBook> AuthorBook { get; set; }
    }
}
