using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Category
    {
        public Category()
        {
            SubCategory = new List<SubCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<SubCategory> SubCategory { get; set; }
    }
}
