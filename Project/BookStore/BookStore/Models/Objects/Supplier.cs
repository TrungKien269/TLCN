using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Supplier
    {
        public Supplier()
        {
            SupplierBook = new HashSet<SupplierBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SupplierBook> SupplierBook { get; set; }
    }
}
