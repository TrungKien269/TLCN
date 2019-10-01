using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class ImageBook
    {
        public string BookId { get; set; }
        public int ImageId { get; set; }
        public string Path { get; set; }

        public Book Book { get; set; }
    }
}
