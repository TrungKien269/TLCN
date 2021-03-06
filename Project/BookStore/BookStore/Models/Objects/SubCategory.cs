﻿using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class SubCategory
    {
        public SubCategory()
        {
            BookCategory = new List<BookCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CateId { get; set; }

        public Category Cate { get; set; }
        public List<BookCategory> BookCategory { get; set; }
    }
}
