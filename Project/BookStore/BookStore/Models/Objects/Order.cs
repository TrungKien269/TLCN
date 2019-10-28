using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new List<OrderDetail>();
        }

        public string Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Total { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
}
