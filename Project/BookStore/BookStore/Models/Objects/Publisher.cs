using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Publisher
    {
        public Publisher()
        {
            PublisherBook = new HashSet<PublisherBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PublisherBook> PublisherBook { get; set; }
    }
}
