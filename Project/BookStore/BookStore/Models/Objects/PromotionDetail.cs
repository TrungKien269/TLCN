using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class PromotionDetail
    {
        public int PromotionId { get; set; }
        public string BookId { get; set; }
        public float Discount { get; set; }

        public Book Book { get; set; }
        public Promotion Promotion { get; set; }
    }
}
