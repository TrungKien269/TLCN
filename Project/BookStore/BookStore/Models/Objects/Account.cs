using System;
using System.Collections.Generic;

namespace BookStore.Models.Objects
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Cookie { get; set; }

        public User IdNavigation { get; set; }
    }
}
