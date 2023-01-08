using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class UserFollower
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int FollowerId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
