using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class PostType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string PostTypeName { get; set; }
    }
}
