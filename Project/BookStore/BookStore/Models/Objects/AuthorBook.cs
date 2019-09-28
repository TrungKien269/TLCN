using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class AuthorBook
    {
        [Column("Book_ID")]
        [StringLength(20)]
        public string BookId { get; set; }
        [Column("Author_ID")]
        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty("AuthorBook")]
        public Author Author { get; set; }
        [ForeignKey("BookId")]
        [InverseProperty("AuthorBook")]
        public Book Book { get; set; }
    }
}
