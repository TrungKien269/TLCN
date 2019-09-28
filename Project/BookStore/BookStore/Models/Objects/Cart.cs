using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class Cart
    {
        public Cart()
        {
            CartBook = new HashSet<CartBook>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("Cart")]
        public User IdNavigation { get; set; }
        [InverseProperty("Cart")]
        public ICollection<CartBook> CartBook { get; set; }
    }
}
