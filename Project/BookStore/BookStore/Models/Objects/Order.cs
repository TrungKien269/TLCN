﻿using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public string Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Total { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
