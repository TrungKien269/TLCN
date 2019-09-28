using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class Comment
    {
        [Column("User_ID")]
        public int UserId { get; set; }
        [Column("Book_ID")]
        [StringLength(20)]
        public string BookId { get; set; }
        [Required]
        [Column("Comment")]
        public string Comment1 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }

        [ForeignKey("BookId")]
        [InverseProperty("Comment")]
        public Book Book { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Comment")]
        public User User { get; set; }
    }
}
