using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class CartBook
    {
        public int CartId { get; set; }
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }
        public DateTime PickedDate { get; set; }

        public Book Book { get; set; }
        public Cart Cart { get; set; }
    }
}
