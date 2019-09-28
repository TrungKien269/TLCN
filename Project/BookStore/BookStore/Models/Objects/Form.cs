﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models.Objects
{
    public partial class Form
    {
        public Form()
        {
            FormBook = new HashSet<FormBook>();
        }

        [Column("ID")]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [InverseProperty("Form")]
        public ICollection<FormBook> FormBook { get; set; }
    }
}
