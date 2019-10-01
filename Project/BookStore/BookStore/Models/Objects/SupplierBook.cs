using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class SupplierBook
    {
        public string BookId { get; set; }
        public int SupplierId { get; set; }

        public Book Book { get; set; }
        public Supplier Supplier { get; set; }
    }
}
