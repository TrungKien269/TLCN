using System;
using System.Collections.Generic;
using System.Text;

namespace DemoProcessRawDataTLCN.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        public DateTime CrearedDateTime { get; set; }
    }
}
