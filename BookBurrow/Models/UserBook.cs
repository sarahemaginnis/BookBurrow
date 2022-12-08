using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class UserBook
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public int UserId { get; set; }
        public UserProfile UserProfile { get; set; }
        public UserPronoun UserPronoun { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? RatingId { get; set; }
        public Rating Rating { get; set; }

        [Required]
        public BookStatus BookStatus { get; set; }
        public string? Review { get; set; }

        [Required]
        public DateTime ReviewCreatedAt { get; set; }

        [Required]
        public DateTime ReviewEditedAt { get; set; }

    }
}
