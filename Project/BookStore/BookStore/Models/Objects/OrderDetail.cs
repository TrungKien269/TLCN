using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class OrderDetail
    {
        [Column("Order_ID")]
        [StringLength(10)]
        public string OrderId { get; set; }
        [Column("Book_ID")]
        [StringLength(20)]
        public string BookId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("BookId")]
        [InverseProperty("OrderDetail")]
        public Book Book { get; set; }
        [ForeignKey("OrderId")]
        [InverseProperty("OrderDetail")]
        public Order Order { get; set; }
    }
}
