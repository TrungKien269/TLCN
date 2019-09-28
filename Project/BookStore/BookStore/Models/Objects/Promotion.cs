using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class Promotion
    {
        public Promotion()
        {
            PromotionDetail = new HashSet<PromotionDetail>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime EndedDate { get; set; }
        public string Description { get; set; }

        [InverseProperty("Promotion")]
        public ICollection<PromotionDetail> PromotionDetail { get; set; }
    }
}
