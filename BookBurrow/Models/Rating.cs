using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        public decimal DisplayValue { get; set; }
    }
}
