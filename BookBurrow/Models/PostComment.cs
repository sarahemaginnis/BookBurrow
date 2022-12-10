using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class PostComment
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public UserProfile UserProfile { get; set; }

        [Required]
        public int PostId { get; set; }

        public UserPost UserPost { get; set; }

        [Required]
        [MaxLength(255)]
        public string Comment { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
