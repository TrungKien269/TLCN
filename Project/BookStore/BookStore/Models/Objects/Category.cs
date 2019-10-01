using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Category
    {
        public Category()
        {
            SubCategory = new HashSet<SubCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategory { get; set; }
    }
}
