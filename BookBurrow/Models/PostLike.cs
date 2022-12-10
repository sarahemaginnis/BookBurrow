using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class PostLike
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public UserProfile UserProfile { get; set; }

        [Required]
        public int PostId { get; set; }

        public UserPost UserPost { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
