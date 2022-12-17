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
        public User User { get; set; } = new User();
        public UserProfile UserProfile { get; set; } = new UserProfile();
        public UserPronoun UserPronoun { get; set; } = new UserPronoun(); 
        public List<UserPost> UserPostsList { get; set; } = new List<UserPost>();
        public List<UserFollower> UserFollowerList { get; set; } = new List<UserFollower>();
        public List<UserFollower> UserFollowingList { get; set; } = new List<UserFollower>();
        public List<UserBook> UserBooksList { get; set; } = new List<UserBook>();
        public UserBook UserBook { get; set; } = new UserBook();
        public Book Book { get; set; } = new Book();
        public BookAuthor BookAuthor { get; set; } = new BookAuthor();
        public Author Author { get; set; } = new Author();
        public BookStatus BookStatus { get; set; }
        public List<BookStatus> BookStatusesList { get; set; }
        public Rating Rating { get; set; } = new Rating(); 
        public UserPost UserPost { get; set; } = new UserPost();
        public UserFollower UserFollower { get; set; } = new UserFollower();
    }
}
