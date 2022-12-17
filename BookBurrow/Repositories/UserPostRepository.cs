using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace BookBurrow.Repositories
{
    public class UserPostRepository : BaseRepository, IUserPostRepository
    {
        public UserPostRepository(IConfiguration configuration) : base(configuration) { }
        public List<UserPost> GetAllOrderedByPostCreationDate()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.id, up.userId, up.postTypeId, 
                            up.bookId, 
                            b.title AS bookTitle, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt AS bookRecordCreatedAt, 
                            b.updatedAt AS bookRecordUpdatedAt,
                            up.title AS postTitle, up.cloudinaryUrl, up.caption AS postCaption, up.source, up.songUrl, up.songUrlSummary, 
                            up.createdAt, up.updatedAt,
                            p.id AS userProfileId, p.profileImageUrl, p.firstName, p.lastName, p.handle, p.pronounId, 
                            pr.pronouns, 
                            p.biography, p.biographyUrl, p.birthday, p.createdAt AS userProfileCreatedAt, p.updatedAt AS userProfileUpdatedAt
                            FROM dbo.UserPost up
                                JOIN dbo.Book b ON up.bookId = b.id
                                JOIN dbo.UserProfile p ON up.userId = p.userId
                                JOIN dbo.UserPronoun pr ON p.pronounId = pr.id
                            ORDER BY createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userPosts = new List<UserPost>();
                        while (reader.Read())
                        {
                            userPosts.Add(new UserPost()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserId = DbUtils.GetInt(reader, "userId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "userProfileId"),
                                    UserId = DbUtils.GetInt(reader, "userId"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                    FirstName = DbUtils.GetString(reader, "firstName"),
                                    LastName = DbUtils.GetString(reader, "lastName"),
                                    Handle = DbUtils.GetString(reader, "handle"),
                                    PronounId = DbUtils.GetNullableInt(reader, "pronounId"),
                                    UserPronoun = new UserPronoun()
                                    {
                                        Id = DbUtils.GetInt(reader, "pronounId"),
                                        Pronouns = DbUtils.GetString(reader, "pronouns"),
                                    },
                                    Biography = DbUtils.GetString(reader, "biography"),
                                    BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                    Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userProfileCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "userProfileUpdatedAt"),
                                },
                                PostType = PostType.FromValue(DbUtils.GetInt(reader, "postTypeId")),
                                BookId = DbUtils.GetInt(reader, "bookId"),
                                Book = new Book()
                                {
                                    Id = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "bookTitle"),
                                    Isbn = DbUtils.GetString(reader, "isbn"),
                                    Description = DbUtils.GetString(reader, "description"),
                                    CoverImageUrl = DbUtils.GetString(reader, "coverImageUrl"),
                                    DatePublished = DbUtils.GetDateTime(reader, "datePublished"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "bookRecordCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "bookRecordUpdatedAt"),
                                },
                                Title = DbUtils.GetString(reader, "postTitle"),
                                CloudinaryUrl = DbUtils.GetString(reader, "cloudinaryUrl"),
                                Caption = DbUtils.GetString(reader, "postCaption"),
                                Source = DbUtils.GetString(reader, "source"),
                                SongUrl = DbUtils.GetString(reader, "songUrl"),
                                SongUrlSummary = DbUtils.GetString(reader, "songUrlSummary"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            });
                        }
                        return userPosts;
                    }
                }
            }
        }

        public UserPost GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.id, up.userId, up.postTypeId, 
                            up.bookId, 
                            b.title AS bookTitle, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt AS bookRecordCreatedAt, 
                            b.updatedAt AS bookRecordUpdatedAt,
                            up.title AS postTitle, up.cloudinaryUrl, up.caption AS postCaption, up.source, up.songUrl, up.songUrlSummary, 
                            up.createdAt, up.updatedAt,
                            p.id AS userProfileId, p.profileImageUrl, p.firstName, p.lastName, p.handle, p.pronounId, 
                            pr.pronouns, 
                            p.biography, p.biographyUrl, p.birthday, p.createdAt AS userProfileCreatedAt, p.updatedAt AS userProfileUpdatedAt
                            FROM dbo.UserPost up
                                JOIN dbo.Book b ON up.bookId = b.id
                                JOIN dbo.UserProfile p ON up.userId = p.userId
                                JOIN dbo.UserPronoun pr ON p.pronounId = pr.id
                        WHERE up.Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserPost userPost = null;
                        if (reader.Read())
                        {
                            userPost = new UserPost()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserId = DbUtils.GetInt(reader, "userId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "userProfileId"),
                                    UserId = DbUtils.GetInt(reader, "userId"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                    FirstName = DbUtils.GetString(reader, "firstName"),
                                    LastName = DbUtils.GetString(reader, "lastName"),
                                    Handle = DbUtils.GetString(reader, "handle"),
                                    PronounId = DbUtils.GetNullableInt(reader, "pronounId"),
                                    UserPronoun = new UserPronoun()
                                    {
                                        Id = DbUtils.GetInt(reader, "pronounId"),
                                        Pronouns = DbUtils.GetString(reader, "pronouns"),
                                    },
                                    Biography = DbUtils.GetString(reader, "biography"),
                                    BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                    Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userProfileCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "userProfileUpdatedAt"),
                                },
                                PostType = PostType.FromValue(DbUtils.GetInt(reader, "postTypeId")),
                                BookId = DbUtils.GetInt(reader, "bookId"),
                                Book = new Book()
                                {
                                    Id = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "bookTitle"),
                                    Isbn = DbUtils.GetString(reader, "isbn"),
                                    Description = DbUtils.GetString(reader, "description"),
                                    CoverImageUrl = DbUtils.GetString(reader, "coverImageUrl"),
                                    DatePublished = DbUtils.GetDateTime(reader, "datePublished"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "bookRecordCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "bookRecordUpdatedAt"),
                                },
                                Title = DbUtils.GetString(reader, "postTitle"),
                                CloudinaryUrl = DbUtils.GetString(reader, "cloudinaryUrl"),
                                Caption = DbUtils.GetString(reader, "postCaption"),
                                Source = DbUtils.GetString(reader, "source"),
                                SongUrl = DbUtils.GetString(reader, "songUrl"),
                                SongUrlSummary = DbUtils.GetString(reader, "songUrlSummary"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            };
                        }
                        return userPost;
                    }
                }
            }
        }

        public void Add(UserPost userPost)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.UserPost (userId, postTypeId, bookId, title, cloudinaryUrl, caption, source, songUrl, 
                            songUrlSummary, createdAt, updatedAt)
                        OUTPUT INSERTED.ID
                        VALUES (@userId, @postTypeId, @bookId, @title, @cloudinaryUrl, @caption, @source, @songUrl, 
                            @songUrlSummary, @createdAt, @updatedAt)
                    ";

                    DbUtils.AddParameter(cmd, "@userId", userPost.UserId);
                    DbUtils.AddParameter(cmd, "@postTypeId", userPost.PostType.Value);
                    DbUtils.AddParameter(cmd, "@bookId", userPost.BookId);
                    DbUtils.AddParameter(cmd, "@title", userPost.Title);
                    DbUtils.AddParameter(cmd, "@cloudinaryUrl", userPost.CloudinaryUrl);
                    DbUtils.AddParameter(cmd, "@caption", userPost.Caption);
                    DbUtils.AddParameter(cmd, "@source", userPost.Source);
                    DbUtils.AddParameter(cmd, "@songUrl", userPost.SongUrl);
                    DbUtils.AddParameter(cmd, "@songUrlSummary", userPost.SongUrlSummary);
                    DbUtils.AddParameter(cmd, "@createdAt", userPost.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", userPost.UpdatedAt);

                    userPost.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserPost userPost)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.UserPost
                            SET userId = @userId, 
                                postTypeId = @postTypeId, 
                                bookId = @bookId, 
                                title = @title, 
                                cloudinaryUrl = @cloudinaryUrl, 
                                caption = @caption, 
                                source = @source, 
                                songUrl = @songUrl, 
                                songUrlSummary = @songUrlSummary, 
                                createdAt = @createdAt, 
                                updatedAt = @updatedAt
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@userId", userPost.UserId);
                    DbUtils.AddParameter(cmd, "@postTypeId", userPost.PostType.Value);
                    DbUtils.AddParameter(cmd, "@bookId", userPost.BookId);
                    DbUtils.AddParameter(cmd, "@title", userPost.Title);
                    DbUtils.AddParameter(cmd, "@cloudinaryUrl", userPost.CloudinaryUrl);
                    DbUtils.AddParameter(cmd, "@caption", userPost.Caption);
                    DbUtils.AddParameter(cmd, "@source", userPost.Source);
                    DbUtils.AddParameter(cmd, "@songUrl", userPost.SongUrl);
                    DbUtils.AddParameter(cmd, "@songUrlSummary", userPost.SongUrlSummary);
                    DbUtils.AddParameter(cmd, "@createdAt", userPost.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", userPost.UpdatedAt);
                    DbUtils.AddParameter(cmd, "@id", userPost.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM dbo.UserPost WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
