using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models.Objects;

namespace BookStore.Models.Statistics
{
    public class TopUser
    {
        public User user { get; set; }
        public int numberOfBook { get; set; }
    }
}
