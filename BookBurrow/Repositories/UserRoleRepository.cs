using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class UserRoleRepository : BaseRepository, IUserRoleRepository
    {
        public UserRoleRepository(IConfiguration configuration) : base(configuration) { }
        public List<UserRole> GetAllOrderedByCreatedAt()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ur.id, ur.userId, ur.roleId, ur.createdAt, ur.updatedAt,
                            up.id AS userProfileId, up.profileImageUrl, up.firstName, up.lastName, up.handle, up.pronounId, p.pronouns, 
                            up.biography, up.biographyUrl, up.birthday, up.createdAt AS userProfileCreatedAt, up.updatedAt AS userProfileUpdatedAt
                            FROM dbo.UserRole ur
                                JOIN dbo.UserProfile up ON ur.userId = up.userId
                                JOIN dbo.UserPronoun p ON up.pronounId = p.id
                        ORDER BY createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userRoles = new List<UserRole>();
                        while (reader.Read())
                        {
                            userRoles.Add(new UserRole()
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
                                Role = Role.FromValue(DbUtils.GetInt(reader, "roleId")),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            });
                        }
                        return userRoles;
                    }
                }
            }
        }

        public UserRole GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ur.id, ur.userId, ur.roleId, ur.createdAt, ur.updatedAt,
                            up.id AS userProfileId, up.profileImageUrl, up.firstName, up.lastName, up.handle, up.pronounId, p.pronouns, 
                            up.biography, up.biographyUrl, up.birthday, up.createdAt AS userProfileCreatedAt, up.updatedAt AS userProfileUpdatedAt
                            FROM dbo.UserRole ur
                                JOIN dbo.UserProfile up ON ur.userId = up.userId
                                JOIN dbo.UserPronoun p ON up.pronounId = p.id
                            WHERE ur.Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserRole userRole = null;
                        if (reader.Read())
                        {
                            userRole = new UserRole()
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
                                Role = Role.FromValue(DbUtils.GetInt(reader, "roleId")),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            };
                        }
                        return userRole;
                    }
                }
            }
        }

        public void Add(UserRole userRole)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.UserRole (userId, roleId, createdAt, updatedAt)
                        OUTPUT INSERTED.ID
                        VALUES (@userId, @roleId, @createdAt, @updatedAt)
                    ";

                    DbUtils.AddParameter(cmd, "@userId", userRole.UserId);
                    DbUtils.AddParameter(cmd, "@roleId", userRole.Role.Value);
                    DbUtils.AddParameter(cmd, "@createdAt", userRole.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", userRole.UpdatedAt);

                    userRole.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserRole userRole)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.UserRole
                            SET userId = @userId, 
                                roleId = @roleId, 
                                createdAt = @createdAt, 
                                updatedAt = @updatedAt
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@userId", userRole.UserId);
                    DbUtils.AddParameter(cmd, "@roleId", userRole.Role.Value);
                    DbUtils.AddParameter(cmd, "@createdAt", userRole.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", userRole.UpdatedAt);
                    DbUtils.AddParameter(cmd, "@id", userRole.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.UserRole WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
