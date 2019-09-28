using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class Rating
    {
        [Column("User_ID")]
        public int UserId { get; set; }
        [Column("Book_ID")]
        [StringLength(20)]
        public string BookId { get; set; }
        public int Point { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [ForeignKey("BookId")]
        [InverseProperty("Rating")]
        public Book Book { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Rating")]
        public User User { get; set; }
    }
}
