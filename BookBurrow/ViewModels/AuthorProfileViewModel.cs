using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;
using BookBurrow.Models;

namespace BookBurrow.ViewModels
{
    public class AuthorProfileViewModel
    {
        public User User { get; set; }
        public UserProfile UserProfile { get; set; }
        public UserBook UserBook { get; set; }
        public List<UserBook> UserBooksList { get; set; }
        public Author Author { get; set; }
        public Book Book { get; set; }
        public List<Book> BooksList { get; set; }
        public BookAuthor BookAuthor { get; set; }
        public List<BookAuthor> BookAuthorsList { get; set; }
        public BookStatus BookStatus { get; set; }
        public List<BookStatus> BookStatusesList { get; set; }
        public PostComment PostComment { get; set; }
        public List<PostComment> PostCommentsList { get; set; }
        public PostFavorite PostFavorite { get; set; }
        public List<PostFavorite> PostFavoritesList { get; set; }
        public PostLike PostLike { get; set; }
        public List<PostLike> PostLikesList { get; set; }
        public PostType PostType { get; set; }
        public Rating Rating { get; set; }
        public Series Series { get; set; }
        public SeriesBook SeriesBook { get; set; }
        public UserFollower UserFollower { get; set; }
        public List<UserFollower> UserFollowerList { get; set; }
        public UserPost UserPost { get; set; }
        public List<UserPost> UserPostsList { get; set; }
        public UserPronoun UserPronoun { get; set; }
    }
}
