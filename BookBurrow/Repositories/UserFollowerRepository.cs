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
                                LEFT JOIN dbo.UserProfile up ON uf.followerId = up.Id
                                LEFT JOIN dbo.UserPronoun p ON up.pronounId = p.id
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
                                LEFT JOIN dbo.UserProfile up ON uf.followerId = up.id
                                LEFT JOIN dbo.UserPronoun p ON up.pronounId = p.id
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
                                FollowerId = DbUtils.GetInt(reader, "followerId"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                            };
                        }
                        return userFollower;
                    }
                }
            }
        }

        public UserFollower VerifyFollowerStatus(int loggedInUserId, int profileUserId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, userId, followerId, createdAt
                        FROM dbo.UserFollower
                        WHERE userId = @profileUserId
                        AND followerId = @loggedInUserId
                    ";
                    DbUtils.AddParameter(cmd, "@profileUserId", profileUserId);
                    DbUtils.AddParameter(cmd, "@loggedInUserId", loggedInUserId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserFollower userFollower = null;
                        if (reader.Read())
                        {
                            userFollower = new UserFollower()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserId = profileUserId,
                                FollowerId = loggedInUserId,
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
                        VALUES (@userId, @followerId, getDate())
                    ";

                    DbUtils.AddParameter(cmd, "@userId", userFollower.UserId);
                    DbUtils.AddParameter(cmd, "@followerId", userFollower.FollowerId);

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
