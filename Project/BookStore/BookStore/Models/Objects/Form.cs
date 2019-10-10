using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Form
    {
        public Form()
        {
            FormBook = new List<FormBook>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<FormBook> FormBook { get; set; }
    }
}
