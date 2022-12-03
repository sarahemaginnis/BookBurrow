using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [MaxLength(254)]
        public string? ProfileImageUrl { get; set; }

        [MaxLength(40)]
        public string? FirstName { get; set; }

        [MaxLength(40)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(40)]
        public string Handle { get; set; }
        public int? PronoundId { get; set; }
        public UserPronoun UserPronoun { get; set; }
        public string? Biography { get; set; }

        [MaxLength(254)]
        public string? BiographyUrl { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
