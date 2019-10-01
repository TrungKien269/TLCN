using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Author
    {
        public Author()
        {
            AuthorBook = new HashSet<AuthorBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AuthorBook> AuthorBook { get; set; }
    }
}
