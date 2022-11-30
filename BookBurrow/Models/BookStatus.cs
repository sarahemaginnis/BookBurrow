using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class BookStatus
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Status { get; set; }
    }
}
