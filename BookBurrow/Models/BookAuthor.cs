using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Models
{
    public class BookAuthor
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
