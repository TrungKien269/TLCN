using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class FormBook
    {
        public string BookId { get; set; }
        public int FormId { get; set; }

        public Book Book { get; set; }
        public Form Form { get; set; }
    }
}
