using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class PostLikeRepository : BaseRepository, IPostLikeRepository
    {
        public PostLikeRepository(IConfiguration configuration) : base(configuration) { }
        public List<PostLike> GetAllOrderedByLikedDate()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT pl.id, pl.userId, pl.postId, pl.createdAt,
                            p.id AS userProfileId, p.profileImageUrl, p.firstName, p.lastName, p.handle, p.pronounId, 
                            pr.pronouns, 
                            p.biography, p.biographyUrl, p.birthday, p.createdAt AS userProfileCreatedAt, p.updatedAt AS userProfileUpdatedAt,
                            up.userId AS postUserId, up.postTypeId, up.bookId, up.title, up.cloudinaryUrl, up.caption, up.source, up.songUrl, up.songUrlSummary, 
                            up.createdAt AS postCreatedAt, up.updatedAt AS postUpdatedAt
                        FROM dbo.PostLike pl
                            JOIN dbo.UserProfile p ON pl.userId = p.userId
                            JOIN dbo.UserPronoun pr ON p.pronounId = pr.id
                            JOIN dbo.UserPost up ON pl.postId = up.id
                        ORDER BY createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var postLikes = new List<PostLike>();
                        while (reader.Read())
                        {
                            postLikes.Add(new PostLike()
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
                                    PronoundId = DbUtils.GetNullableInt(reader, "pronounId"),
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
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                            });
                        }
                        return postLikes;
                    }
                }
            }
        }

        public PostLike GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT pl.id, pl.userId, pl.postId, pl.createdAt,
                            p.id AS userProfileId, p.profileImageUrl, p.firstName, p.lastName, p.handle, p.pronounId, 
                            pr.pronouns, 
                            p.biography, p.biographyUrl, p.birthday, p.createdAt AS userProfileCreatedAt, p.updatedAt AS userProfileUpdatedAt,
                            up.userId AS postUserId, up.postTypeId, up.bookId, up.title, up.cloudinaryUrl, up.caption, up.source, up.songUrl, up.songUrlSummary, 
                            up.createdAt AS postCreatedAt, up.updatedAt AS postUpdatedAt
                        FROM dbo.PostLike pl
                            JOIN dbo.UserProfile p ON pl.userId = p.userId
                            JOIN dbo.UserPronoun pr ON p.pronounId = pr.id
                            JOIN dbo.UserPost up ON pl.postId = up.id
                        WHERE pl.id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        PostLike postLike = null;
                        if (reader.Read())
                        {
                            postLike = new PostLike()
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
                                    PronoundId = DbUtils.GetNullableInt(reader, "pronounId"),
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
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                            };
                        }

                        return postLike;
                    }
                }
            }
        }

        public void Add(PostLike postLike)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.PostLike (userId, postId, createdAt)
                        OUTPUT INSERTED.ID
                        VALUES (@userId, @postId, @createdAt)
                    ";

                    DbUtils.AddParameter(cmd, "@userId", postLike.UserId);
                    DbUtils.AddParameter(cmd, "@postId", postLike.PostId);
                    DbUtils.AddParameter(cmd, "@createdAt", postLike.CreatedAt);

                    postLike.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(PostLike postLike)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.PostLike
                            SET userId = @userId,
                                postId = @postId,
                                createdAt = @createdAt
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@userId", postLike.UserId);
                    DbUtils.AddParameter(cmd, "@postId", postLike.PostId);
                    DbUtils.AddParameter(cmd, "@createdAt", postLike.CreatedAt);
                    DbUtils.AddParameter(cmd, "@id", postLike.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.PostLike WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
