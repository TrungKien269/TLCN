using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Publisher
    {
        public Publisher()
        {
            PublisherBook = new List<PublisherBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<PublisherBook> PublisherBook { get; set; }
    }
}
