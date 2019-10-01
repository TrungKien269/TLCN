using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class PublisherBook
    {
        public string BookId { get; set; }
        public int PublisherId { get; set; }

        public Book Book { get; set; }
        public Publisher Publisher { get; set; }
    }
}
