using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class UserProfileRepository : BaseRepository, IUserProfileRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserProfile> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT u.id, u.userId, u.profileImageUrl, u.firstName, u.lastName, u.handle, u.pronounId, u.biography, u.biographyUrl, u.birthday, u.createdAt, u.updatedAt,
                            up.pronouns
                        FROM dbo.UserProfile u
                        LEFT JOIN dbo.UserPronoun up ON u.pronounId = up.id
                        ORDER BY createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            var pronounId = DbUtils.GetNullableInt(reader, "pronounId");
                            userProfiles.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserId = DbUtils.GetInt(reader, "userId"),
                                ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                FirstName = DbUtils.GetString(reader, "firstName"),
                                LastName = DbUtils.GetString(reader, "lastName"),
                                Handle = DbUtils.GetString(reader, "handle"),
                                PronounId = pronounId,
                                UserPronoun = pronounId == null ? null : new UserPronoun()
                                {
                                    Id = DbUtils.GetInt(reader, "pronounId"),
                                    Pronouns = DbUtils.GetString(reader, "pronouns"),
                                },
                                Biography = DbUtils.GetString(reader, "biography"),
                                BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            });
                        }

                        return userProfiles;
                    }
                }
            }
        }

        public List<UserProfile> Search(string criterion)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var sql = @"
                        SELECT u.id, u.userId, u.profileImageUrl, u.firstName, u.lastName, u.handle, u.pronounId, u.biography, u.biographyUrl, u.birthday, u.createdAt, u.updatedAt,
                            up.pronouns
                        FROM dbo.UserProfile u
                            LEFT JOIN dbo.UserPronoun up ON u.pronounId = up.id
                        WHERE u.firstName LIKE @criterion OR u.lastName LIKE @criterion OR u.handle LIKE @criterion OR u.biography LIKE @criterion
                    ";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@criterion", criterion + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfiles = new List<UserProfile>();
                        while (reader.Read())
                        {
                            var pronounId = DbUtils.GetNullableInt(reader, "pronounId");
                            userProfiles.Add(new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserId = DbUtils.GetInt(reader, "userId"),
                                ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                FirstName = DbUtils.GetString(reader, "firstName"),
                                LastName = DbUtils.GetString(reader, "lastName"),
                                Handle = DbUtils.GetString(reader, "handle"),
                                PronounId = pronounId,
                                UserPronoun = pronounId == null ? null : new UserPronoun()
                                {
                                    Id = DbUtils.GetInt(reader, "pronounId"),
                                    Pronouns = DbUtils.GetString(reader, "pronouns"),
                                },
                                Biography = DbUtils.GetString(reader, "biography"),
                                BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            });
                        }
                        return userProfiles;
                    }
                }
            }
        }

        public UserProfile GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT u.userId, u.profileImageUrl, u.firstName, u.lastName, u.handle, u.pronounId, u.biography, u.biographyUrl, u.birthday, u.createdAt, u.updatedAt,
                            up.pronouns
                        FROM dbo.UserProfile u
                        LEFT JOIN dbo.UserPronoun up ON u.pronounId = up.id
                        WHERE u.Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile userProfile = null;
                        if (reader.Read())
                        {
                            var pronounId = DbUtils.GetNullableInt(reader, "pronounId");
                            userProfile = new UserProfile()
                            {
                                Id = id,
                                UserId = DbUtils.GetInt(reader, "userId"),
                                ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                FirstName = DbUtils.GetString(reader, "firstName"),
                                LastName = DbUtils.GetString(reader, "lastName"),
                                Handle = DbUtils.GetString(reader, "handle"),
                                PronounId = pronounId,
                                UserPronoun = pronounId == null ? null : new UserPronoun()
                                {
                                    Id = DbUtils.GetInt(reader, "pronounId"),
                                    Pronouns = DbUtils.GetString(reader, "pronouns"),
                                },
                                Biography = DbUtils.GetString(reader, "biography"),
                                BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            };
                        }
                        return userProfile;
                    }
                }
            }
        }

        public UserProfile GetByUserId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT u.id, u.userId, u.profileImageUrl, u.firstName, u.lastName, u.handle, u.pronounId, u.biography, u.biographyUrl, u.birthday, u.createdAt, u.updatedAt,
                            up.pronouns
                        FROM dbo.UserProfile u
                        LEFT JOIN dbo.UserPronoun up ON u.pronounId = up.id
                        WHERE u.userId = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfile userProfile = null;
                        if (reader.Read())
                        {
                            var pronounId = DbUtils.GetNullableInt(reader, "pronounId");
                            userProfile = new UserProfile()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserId = id,
                                ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                FirstName = DbUtils.GetString(reader, "firstName"),
                                LastName = DbUtils.GetString(reader, "lastName"),
                                Handle = DbUtils.GetString(reader, "handle"),
                                PronounId = pronounId,
                                UserPronoun = pronounId == null ? null : new UserPronoun()
                                {
                                    Id = DbUtils.GetInt(reader, "pronounId"),
                                    Pronouns = DbUtils.GetString(reader, "pronouns"),
                                },
                                Biography = DbUtils.GetString(reader, "biography"),
                                BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            };
                        }
                        return userProfile;
                    }
                }
            }
        }

        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.UserProfile (userId, profileImageUrl, firstName, lastName, handle, pronounId, biography, biographyUrl, birthday, createdAt, updatedAt)
                        OUTPUT INSERTED.id
                        VALUES (@userId, @profileImageUrl, @firstName, @lastName, @handle, @pronounId, @biography, @biographyUrl, @birthday, getDate(), getDate())
                    ";
                    DbUtils.AddParameter(cmd, "@userId", userProfile.UserId);
                    DbUtils.AddParameter(cmd, "@profileImageUrl", userProfile.ProfileImageUrl);
                    DbUtils.AddParameter(cmd, "@firstName", userProfile.FirstName);
                    DbUtils.AddParameter(cmd, "@lastName", userProfile.LastName);
                    DbUtils.AddParameter(cmd, "@handle", userProfile.Handle);
                    DbUtils.AddParameter(cmd, "@pronounId", userProfile.PronounId);
                    DbUtils.AddParameter(cmd, "@biography", userProfile.Biography);
                    DbUtils.AddParameter(cmd, "@biographyUrl", userProfile.BiographyUrl);
                    DbUtils.AddParameter(cmd, "@birthday", userProfile.Birthday);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.UserProfile
                            SET userId = @userId,
                                profileImageUrl = @profileImageUrl,
                                firstName = @firstName,
                                lastName = @lastName,
                                handle = @handle,
                                pronounId = @pronounId,
                                biography = @biography,
                                biographyUrl = @biographyUrl,
                                birthday = @birthday,
                                createdAt = @createdAt,
                                updatedAt = getDate()
                        WHERE Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@userId", userProfile.UserId);
                    DbUtils.AddParameter(cmd, "@profileImageUrl", userProfile.ProfileImageUrl);
                    DbUtils.AddParameter(cmd, "@firstName", userProfile.FirstName);
                    DbUtils.AddParameter(cmd, "@lastName", userProfile.LastName);
                    DbUtils.AddParameter(cmd, "@handle", userProfile.Handle);
                    DbUtils.AddParameter(cmd, "@pronounId", userProfile.PronounId);
                    DbUtils.AddParameter(cmd, "@biography", userProfile.Biography);
                    DbUtils.AddParameter(cmd, "@biographyUrl", userProfile.BiographyUrl);
                    DbUtils.AddParameter(cmd, "@birthday", userProfile.Birthday);
                    DbUtils.AddParameter(cmd, "@createdAt", userProfile.CreatedAt);
                    DbUtils.AddParameter(cmd, "@id", userProfile.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.UserProfile WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
