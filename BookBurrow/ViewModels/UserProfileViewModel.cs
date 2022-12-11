using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using BookBurrow.Models;

namespace BookBurrow.ViewModels
{
    public class UserProfileViewModel
    {
        public User User { get; set; }
        public UserProfile UserProfile { get; set; }
        public UserPronoun UserPronoun { get; set; }
        public List<UserPost> UserPostsList { get; set; }
        public List<UserFollower> UserFollowerList { get; set; }
        public List<UserFollower> UserFollowingList { get; set; }
        public List<UserBook> UserBooksList { get; set; }
        public UserBook UserBook { get; set; }
        public Book Book { get; set; }
        public BookAuthor BookAuthor { get; set; }
        public Author Author { get; set; }
        public BookStatus BookStatus { get; set; }
        public List<BookStatus> BookStatusesList { get; set; }
        public Rating Rating { get; set; }
        public UserPost UserPost { get; set; }
        public UserFollower UserFollower { get; set; }
    }
}
