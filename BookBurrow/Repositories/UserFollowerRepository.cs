using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class UserFollowerRepository : BaseRepository, IUserFollowerRepository
    {
        public UserFollowerRepository(IConfiguration configuration) : base(configuration) { }
        public List<UserFollower> GetAllFollowerProfilesOrderedByFollowDate()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT uf.id, uf.userId, uf.followerId, uf.createdAt,
                                up.id AS userFollowerProfileId, up.userId AS userFollowerUserId, up.profileImageUrl, up.firstName, up.lastName, 
                                up.handle, up.pronounId, up.biography, up.biographyUrl, up.birthday, up.createdAt AS userFollowerProfileCreatedAt, up.updatedAt,
                                p.pronouns    
                        FROM dbo.UserFollower uf
                                JOIN dbo.UserProfile up ON uf.followerId = up.Id
                                JOIN dbo.UserPronoun p ON up.pronounId = p.id
                        ORDER BY createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userFollowers = new List<UserFollower>();
                        while (reader.Read())
                        {
                            userFollowers.Add(new UserFollower()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserId = DbUtils.GetInt(reader, "userId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "userFollowerProfileId"),
                                    UserId = DbUtils.GetInt(reader, "userFollowerUserId"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                    FirstName = DbUtils.GetString(reader, "firstName"),
                                    LastName = DbUtils.GetString(reader, "lastName"),
                                    Handle = DbUtils.GetString(reader, "handle"),
                                    PronounId = DbUtils.GetNullableInt(reader, "pronounId"),
                                    UserPronoun = new UserPronoun()
                                    {
                                        Id = DbUtils.GetInt(reader, "pronounId"),
                                        Pronouns = DbUtils.GetString(reader, "pronouns")
                                    },
                                    Biography = DbUtils.GetString(reader, "biography"),
                                    BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                    Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userFollowerProfileCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                },
                                FollowerId = DbUtils.GetInt(reader, "followerId"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                            });
                        }
                        return userFollowers;
                    }
                }
            }
        }

        public UserFollower GetFollowerProfileById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT uf.userId, uf.followerId, uf.createdAt,
                                up.id AS userFollowerProfileId, up.userId AS userFollowerUserId, up.profileImageUrl, up.firstName, up.lastName, 
                                up.handle, up.pronounId, up.biography, up.biographyUrl, up.birthday, up.createdAt AS userFollowerProfileCreatedAt, up.updatedAt,
                                p.id AS pronounId, p.pronouns    
                            FROM dbo.UserFollower uf
                                JOIN dbo.UserProfile up ON uf.followerId = up.id
                                JOIN dbo.UserPronoun p ON up.pronounId = p.id
                        WHERE uf.Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserFollower userFollower = null;
                        if (reader.Read())
                        {
                            userFollower = new UserFollower()
                            {
                                Id = id,
                                UserId = DbUtils.GetInt(reader, "userId"),
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "userFollowerProfileId"),
                                    UserId = DbUtils.GetInt(reader, "userFollowerUserId"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                    FirstName = DbUtils.GetString(reader, "firstName"),
                                    LastName = DbUtils.GetString(reader, "lastName"),
                                    Handle = DbUtils.GetString(reader, "handle"),
                                    PronounId = DbUtils.GetNullableInt(reader, "pronounId"),
                                    UserPronoun = new UserPronoun()
                                    {
                                        Id = DbUtils.GetInt(reader, "pronounId"),
                                        Pronouns = DbUtils.GetString(reader, "pronouns")
                                    },
                                    Biography = DbUtils.GetString(reader, "biography"),
                                    BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                    Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userFollowerProfileCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                },
                                FollowerId = DbUtils.GetInt(reader, "followerId"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                            };
                        }
                        return userFollower;
                    }
                }
            }
        }

        public void Add(UserFollower userFollower)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.UserFollower (userId, followerId, createdAt)
                        OUTPUT INSERTED.id
                        VALUES (@userId, @followerId, @createdAt)
                    ";

                    DbUtils.AddParameter(cmd, "@userId", userFollower.UserId);
                    DbUtils.AddParameter(cmd, "@followerId", userFollower.FollowerId);
                    DbUtils.AddParameter(cmd, "@createdAt", userFollower.CreatedAt);

                    userFollower.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserFollower userFollower)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.UserFollower
                            SET userId = @userId,
                                followerId = @followerId,
                                createdAt = @createdAt
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@userId", userFollower.UserId);
                    DbUtils.AddParameter(cmd, "@followerId", userFollower.FollowerId);
                    DbUtils.AddParameter(cmd, "@createdAt", userFollower.CreatedAt);
                    DbUtils.AddParameter(cmd, "@id", userFollower.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.UserFollower WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
