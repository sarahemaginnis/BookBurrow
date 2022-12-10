using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class UserPost
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public UserProfile UserProfile { get; set; }

        [Required]
        public PostType PostType { get; set; }

        public int? BookId { get; set; }
        public Book Book { get; set; }

        [MaxLength(255)]
        public string? Title { get; set; }
        public string? CloudinaryUrl { get; set; }
        public string? Caption { get; set; }
        public string? Source { get; set; }
        public string? SongUrl { get; set; }
        public string? SongUrlSummary { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
