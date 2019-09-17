using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DemoCrawlBookStore.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Int32 Price { get; set; }
        public string Supplier{get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int? ReleaseYear { get; set; }
        public double? Weight { get; set; }
        public int? NumOfPage { get; set; }
        public string Form { get; set; }
        public string Image { get; set; }
        public string Summary { get; set; }
    }
}
