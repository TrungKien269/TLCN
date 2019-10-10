using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Book
    {
        public Book()
        {
            AuthorBook = new List<AuthorBook>();
            BookCategory = new List<BookCategory>();
            CartBook = new List<CartBook>();
            Comment = new List<Comment>();
            FormBook = new List<FormBook>();
            ImageBook = new List<ImageBook>();
            OrderDetail = new List<OrderDetail>();
            PromotionDetail = new List<PromotionDetail>();
            PublisherBook = new List<PublisherBook>();
            Rating = new List<Rating>();
            SupplierBook = new List<SupplierBook>();
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

        public List<AuthorBook> AuthorBook { get; set; }
        public List<BookCategory> BookCategory { get; set; }
        public List<CartBook> CartBook { get; set; }
        public List<Comment> Comment { get; set; }
        public List<FormBook> FormBook { get; set; }
        public List<ImageBook> ImageBook { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<PromotionDetail> PromotionDetail { get; set; }
        public List<PublisherBook> PublisherBook { get; set; }
        public List<Rating> Rating { get; set; }
        public List<SupplierBook> SupplierBook { get; set; }
    }
}
