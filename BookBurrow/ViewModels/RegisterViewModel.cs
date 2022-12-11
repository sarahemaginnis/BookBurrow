using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using BookBurrow.Models;

namespace BookBurrow.ViewModels
{
    public class RegisterViewModel
    {
        public User User { get; set; }
        public UserProfile UserProfile { get; set; }
        public List<UserProfile> UserProfilesList { get; set; }
        public UserRole UserRole { get; set; }
    }
}
