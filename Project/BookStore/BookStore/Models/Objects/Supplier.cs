using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Supplier
    {
        public Supplier()
        {
            SupplierBook = new List<SupplierBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<SupplierBook> SupplierBook { get; set; }
    }
}
