using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class OrderDetail
    {
        public string OrderId { get; set; }
        public string BookId { get; set; }
        public int Quantity { get; set; }

        public Book Book { get; set; }
        public Order Order { get; set; }
    }
}
