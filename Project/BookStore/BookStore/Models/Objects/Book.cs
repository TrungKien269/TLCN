using System;
using System.Collections.Generic;

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

        public string Id { get; set; }
        public string Name { get; set; }
        public int OriginalPrice { get; set; }
        public int CurrentPrice { get; set; }
        public int? ReleaseYear { get; set; }
        public float? Weight { get; set; }
        public int? NumOfPage { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
        public string Status { get; set; }

        public ICollection<AuthorBook> AuthorBook { get; set; }
        public ICollection<BookCategory> BookCategory { get; set; }
        public ICollection<CartBook> CartBook { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<FormBook> FormBook { get; set; }
        public ICollection<ImageBook> ImageBook { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
        public ICollection<PromotionDetail> PromotionDetail { get; set; }
        public ICollection<PublisherBook> PublisherBook { get; set; }
        public ICollection<Rating> Rating { get; set; }
        public ICollection<SupplierBook> SupplierBook { get; set; }
    }
}
