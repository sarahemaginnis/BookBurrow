using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class UserPronoun
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string Pronouns { get; set; }
    }
}
