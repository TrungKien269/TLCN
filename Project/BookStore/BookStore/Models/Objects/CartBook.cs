using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class CartBook
    {
        [Column("Cart_ID")]
        public int CartId { get; set; }
        [Column("Book_ID")]
        [StringLength(20)]
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public int SubTotal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PickedDate { get; set; }

        [ForeignKey("BookId")]
        [InverseProperty("CartBook")]
        public Book Book { get; set; }
        [ForeignKey("CartId")]
        [InverseProperty("CartBook")]
        public Cart Cart { get; set; }
    }
}
