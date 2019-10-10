using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Promotion
    {
        public Promotion()
        {
            PromotionDetail = new List<PromotionDetail>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndedDate { get; set; }
        public string Description { get; set; }

        public List<PromotionDetail> PromotionDetail { get; set; }
    }
}
