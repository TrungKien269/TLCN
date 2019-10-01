using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class BookCategory
    {
        public string BookId { get; set; }
        public int CateId { get; set; }

        public Book Book { get; set; }
        public SubCategory Cate { get; set; }
    }
}
