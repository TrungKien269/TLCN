using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class BookCategory
    {
        [Column("BookID")]
        [StringLength(20)]
        public string BookId { get; set; }
        [Column("CateID")]
        public int CateId { get; set; }

        [ForeignKey("BookId")]
        [InverseProperty("BookCategory")]
        public Book Book { get; set; }
        [ForeignKey("CateId")]
        [InverseProperty("BookCategory")]
        public SubCategory Cate { get; set; }
    }
}
