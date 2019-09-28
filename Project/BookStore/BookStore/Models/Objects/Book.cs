using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class Book
    {
        public Book()
        {
            AuthorBook = new HashSet<AuthorBook>();
            BookCategory = new HashSet<BookCategory>();
            CartBook = new HashSet<CartBook>();
            Comment = new HashSet<Comment>();
            FormBook = new HashSet<FormBook>();
            ImageBook = new HashSet<ImageBook>();
            OrderDetail = new HashSet<OrderDetail>();
            PromotionDetail = new HashSet<PromotionDetail>();
            PublisherBook = new HashSet<PublisherBook>();
            Rating = new HashSet<Rating>();
            SupplierBook = new HashSet<SupplierBook>();
        }

        [Column("ID")]
        [StringLength(20)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int OriginalPrice { get; set; }
        public int CurrentPrice { get; set; }
        public int? ReleaseYear { get; set; }
        public float? Weight { get; set; }
        public int? NumOfPage { get; set; }
        [Required]
        public string Image { get; set; }
        public string Summary { get; set; }
        [Required]
        [StringLength(100)]
        public string Status { get; set; }

        [InverseProperty("Book")]
        public ICollection<AuthorBook> AuthorBook { get; set; }
        [InverseProperty("Book")]
        public ICollection<BookCategory> BookCategory { get; set; }
        [InverseProperty("Book")]
        public ICollection<CartBook> CartBook { get; set; }
        [InverseProperty("Book")]
        public ICollection<Comment> Comment { get; set; }
        [InverseProperty("Book")]
        public ICollection<FormBook> FormBook { get; set; }
        [InverseProperty("Book")]
        public ICollection<ImageBook> ImageBook { get; set; }
        [InverseProperty("Book")]
        public ICollection<OrderDetail> OrderDetail { get; set; }
        [InverseProperty("Book")]
        public ICollection<PromotionDetail> PromotionDetail { get; set; }
        [InverseProperty("Book")]
        public ICollection<PublisherBook> PublisherBook { get; set; }
        [InverseProperty("Book")]
        public ICollection<Rating> Rating { get; set; }
        [InverseProperty("Book")]
        public ICollection<SupplierBook> SupplierBook { get; set; }
    }
}
