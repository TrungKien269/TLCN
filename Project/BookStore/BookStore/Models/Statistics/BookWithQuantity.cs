using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Objects;

namespace BookStore.Models.Statistics
{
    public class BookWithQuantity
    {
        public Book book { get; set; }
        public int quantity { get; set; }
    }
}
