using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Promotion
    {
        public Promotion()
        {
            PromotionDetail = new HashSet<PromotionDetail>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EndedDate { get; set; }
        public string Description { get; set; }

        public ICollection<PromotionDetail> PromotionDetail { get; set; }
    }
}
