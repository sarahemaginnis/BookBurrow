using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace BookBurrow.Repositories
{
    public class PostCommentRepository : BaseRepository, IPostCommentRepository
    {
        public PostCommentRepository(IConfiguration configuration) : base(configuration) { }
        public List<PostComment> GetAllOrderedByCommentDate()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT pc.id, pc.userId, pc.postId, pc.comment, pc.createdAt, pc.updatedAt,
                            p.id AS userProfileId, p.profileImageUrl, p.firstName, p.lastName, p.handle, p.pronounId, 
                            pr.pronouns, 
                            p.biography, p.biographyUrl, p.birthday, p.createdAt AS userProfileCreatedAt, p.updatedAt AS userProfileUpdatedAt,
                            up.userId AS postUserId, up.postTypeId, up.bookId, up.title, up.cloudinaryUrl, up.caption, up.source, up.songUrl, up.songUrlSummary, 
                            up.createdAt AS postCreatedAt, up.updatedAt AS postUpdatedAt
                            FROM dbo.PostComment pc
                                JOIN dbo.UserProfile p ON pc.userId = p.userId
                                JOIN dbo.UserPronoun pr ON p.pronounId = pr.id
                                JOIN dbo.UserPost up ON pc.postId = up.id
                        ORDER BY createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var comments = new List<PostComment>();
                        while (reader.Read())
                        {
                            comments.Add(new PostComment()
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
                                PostId = DbUtils.GetInt(reader, "postId"),
                                UserPost = new UserPost()
                                {
                                    Id = DbUtils.GetInt(reader, "postId"),
                                    UserId = DbUtils.GetInt(reader, "postUserId"),
                                    PostType = PostType.FromValue(DbUtils.GetInt(reader, "postTypeId")),
                                    BookId = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "title"),
                                    CloudinaryUrl = DbUtils.GetString(reader, "cloudinaryUrl"),
                                    Caption = DbUtils.GetString(reader, "caption"),
                                    Source = DbUtils.GetString(reader, "source"),
                                    SongUrl = DbUtils.GetString(reader, "songUrl"),
                                    SongUrlSummary = DbUtils.GetString(reader, "songUrlSummary"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "postCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "postUpdatedAt"),
                                },
                                Comment = DbUtils.GetString(reader, "comment"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            });
                        }
                        return comments;
                    }
                }
            }
        }

        public PostComment GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT pc.id, pc.userId, pc.postId, pc.comment, pc.createdAt, pc.updatedAt,
                            p.id AS userProfileId, p.profileImageUrl, p.firstName, p.lastName, p.handle, p.pronounId, 
                            pr.pronouns, 
                            p.biography, p.biographyUrl, p.birthday, p.createdAt AS userProfileCreatedAt, p.updatedAt AS userProfileUpdatedAt,
                            up.userId AS postUserId, up.postTypeId, up.bookId, up.title, up.cloudinaryUrl, up.caption, up.source, up.songUrl, up.songUrlSummary, 
                            up.createdAt AS postCreatedAt, up.updatedAt AS postUpdatedAt
                            FROM dbo.PostComment pc
                                JOIN dbo.UserProfile p ON pc.userId = p.userId
                                JOIN dbo.UserPronoun pr ON p.pronounId = pr.id
                                JOIN dbo.UserPost up ON pc.postId = up.id
                        WHERE pc.id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        PostComment postComment = null;
                        if (reader.Read())
                        {
                            postComment = new PostComment()
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
                                PostId = DbUtils.GetInt(reader, "postId"),
                                UserPost = new UserPost()
                                {
                                    Id = DbUtils.GetInt(reader, "postId"),
                                    UserId = DbUtils.GetInt(reader, "postUserId"),
                                    PostType = PostType.FromValue(DbUtils.GetInt(reader, "postTypeId")),
                                    BookId = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "title"),
                                    CloudinaryUrl = DbUtils.GetString(reader, "cloudinaryUrl"),
                                    Caption = DbUtils.GetString(reader, "caption"),
                                    Source = DbUtils.GetString(reader, "source"),
                                    SongUrl = DbUtils.GetString(reader, "songUrl"),
                                    SongUrlSummary = DbUtils.GetString(reader, "songUrlSummary"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "postCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "postUpdatedAt"),
                                },
                                Comment = DbUtils.GetString(reader, "comment"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            };
                        }
                        return postComment;
                    }
                }
            }
        }

        public void Add(PostComment postComment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.PostComment (userId, postId, comment, createdAt, updatedAt)
                        OUTPUT INSERTED.ID
                        VALUES (@userId, @postId, @comment, @createdAt, @updatedAt)
                    ";

                    DbUtils.AddParameter(cmd, "@userId", postComment.UserId);
                    DbUtils.AddParameter(cmd, "@postId", postComment.PostId);
                    DbUtils.AddParameter(cmd, "@comment", postComment.Comment);
                    DbUtils.AddParameter(cmd, "@createdAt", postComment.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", postComment.UpdatedAt);

                    postComment.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(PostComment postComment)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.PostComment
                            SET userId = @userId,
                                postId = @postId,
                                comment = @comment,
                                createdAt = @createdAt,
                                updatedAt = @updatedAt
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@userId", postComment.UserId);
                    DbUtils.AddParameter(cmd, "@postId", postComment.PostId);
                    DbUtils.AddParameter(cmd, "@comment", postComment.Comment);
                    DbUtils.AddParameter(cmd, "@createdAt", postComment.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", postComment.UpdatedAt);
                    DbUtils.AddParameter(cmd, "@id", postComment.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.PostComment WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
