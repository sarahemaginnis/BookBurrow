using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class PostFavoriteRepository : BaseRepository, IPostFavoriteRepository
    {
        public PostFavoriteRepository(IConfiguration configuration) : base(configuration) { }
        public List<PostFavorite> GetAllOrderedByFavoritedDate()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT pf.id, pf.userId, pf.postId, pf.createdAt,
                            p.id AS userProfileId, p.profileImageUrl, p.firstName, p.lastName, p.handle, p.pronounId, 
                            pr.pronouns, 
                            p.biography, p.biographyUrl, p.birthday, p.createdAt AS userProfileCreatedAt, p.updatedAt AS userProfileUpdatedAt,
                            up.userId AS postUserId, up.postTypeId, up.bookId, up.title, up.cloudinaryUrl, up.caption, up.source, up.songUrl, up.songUrlSummary, 
                            up.createdAt AS postCreatedAt, up.updatedAt AS postUpdatedAt
                        FROM dbo.PostFavorite pf
                            JOIN dbo.UserProfile p ON pf.userId = p.userId
                            JOIN dbo.UserPronoun pr ON p.pronounId = pr.id
                            JOIN dbo.UserPost up ON pf.postId = up.id
                        ORDER BY createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var postFavorites = new List<PostFavorite>();
                        while (reader.Read())
                        {
                            postFavorites.Add(new PostFavorite()
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
                        return postFavorites;
                    }
                }
            }
        }

        public PostFavorite GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT pf.id, pf.userId, pf.postId, pf.createdAt,
                            p.id AS userProfileId, p.profileImageUrl, p.firstName, p.lastName, p.handle, p.pronounId, 
                            pr.pronouns, 
                            p.biography, p.biographyUrl, p.birthday, p.createdAt AS userProfileCreatedAt, p.updatedAt AS userProfileUpdatedAt,
                            up.userId AS postUserId, up.postTypeId, up.bookId, up.title, up.cloudinaryUrl, up.caption, up.source, up.songUrl, up.songUrlSummary, 
                            up.createdAt AS postCreatedAt, up.updatedAt AS postUpdatedAt
                        FROM dbo.PostFavorite pf
                            JOIN dbo.UserProfile p ON pf.userId = p.userId
                            JOIN dbo.UserPronoun pr ON p.pronounId = pr.id
                            JOIN dbo.UserPost up ON pf.postId = up.id
                        WHERE pf.id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        PostFavorite postFavorite = null;
                        if (reader.Read())
                        {
                            postFavorite = new PostFavorite()
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
                        return postFavorite;
                    }
                }
            }
        }

        public void Add(PostFavorite postFavorite)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.PostFavorite (userId, postId, createdAt)
                        OUTPUT INSERTED.ID
                        VALUES (@userId, @postId, @createdAt)
                    ";

                    DbUtils.AddParameter(cmd, "@userId", postFavorite.UserId);
                    DbUtils.AddParameter(cmd, "@postId", postFavorite.PostId);
                    DbUtils.AddParameter(cmd, "@createdAt", postFavorite.CreatedAt);

                    postFavorite.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(PostFavorite postFavorite)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.PostFavorite
                            SET userId = @userId,
                                postId = @postId,
                                createdAt = @createdAt
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@userId", postFavorite.UserId);
                    DbUtils.AddParameter(cmd, "@postId", postFavorite.PostId);
                    DbUtils.AddParameter(cmd, "@createdAt", postFavorite.CreatedAt);
                    DbUtils.AddParameter(cmd, "@id", postFavorite.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.PostFavorite WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
