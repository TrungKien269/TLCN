using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Cart
    {
        public Cart()
        {
            CartBook = new HashSet<CartBook>();
        }

        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public User IdNavigation { get; set; }
        public ICollection<CartBook> CartBook { get; set; }
    }
}
