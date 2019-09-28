using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class SupplierBook
    {
        [Column("Book_ID")]
        [StringLength(20)]
        public string BookId { get; set; }
        [Column("Supplier_ID")]
        public int SupplierId { get; set; }

        [ForeignKey("BookId")]
        [InverseProperty("SupplierBook")]
        public Book Book { get; set; }
        [ForeignKey("SupplierId")]
        [InverseProperty("SupplierBook")]
        public Supplier Supplier { get; set; }
    }
}
