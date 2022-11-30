using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class Author
    {
        public int Id { get; set; }
        public int? UserId { get; set; }

        [MaxLength(40)]
        public string? FirstName { get; set; }

        [MaxLength(40)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(254)]
        public string ProfileImageUrl { get; set; }
    }
}
