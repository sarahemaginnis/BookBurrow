using BookBurrow.Models;
using BookBurrow.Utils;
using BookBurrow.ViewModels;
using CloudinaryDotNet.Actions;
using Microsoft.Data.SqlClient;
using Role = BookBurrow.Models.Role;

namespace BookBurrow.Repositories
{
    public class RegisterViewModelRepository : BaseRepository, IRegisterViewModelRepository
    {
        public RegisterViewModelRepository(IConfiguration configuration) : base(configuration) { }
        public RegisterViewModel GetUserById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT firebaseUID, email, createdAt, updatedAt
                        FROM dbo.[User]
                        WHERE Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        RegisterViewModel registerViewModel = null;
                        if (reader.Read())
                        {
                            registerViewModel = new RegisterViewModel()
                            {
                                User = new User()
                                {
                                    Id = id,
                                    FirebaseUID = DbUtils.GetString(reader, "firebaseUID"),
                                    Email = DbUtils.GetString(reader, "email"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                }
                            };
                        }
                        return registerViewModel;
                    }
                }
            }
        }

        public List<RegisterViewModel> GetAllUserProfiles()
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
                        JOIN dbo.UserPronoun up ON u.pronounId = up.id
                        ORDER BY createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfiles = new List<RegisterViewModel>();
                        while (reader.Read())
                        {
                            userProfiles.Add(new RegisterViewModel()
                            {
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
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
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                }
                            });
                        }

                        return userProfiles;
                    }
                }
            }
        }

        public RegisterViewModel GetUserProfileById(int id)
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
                        JOIN dbo.UserPronoun up ON u.pronounId = up.id
                        WHERE u.Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        RegisterViewModel registerViewModel = null;
                        if (reader.Read())
                        {
                            registerViewModel = new RegisterViewModel()
                            {
                                UserProfile = new UserProfile()
                                {
                                    Id = id,
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
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                }
                            };
                        }
                        return registerViewModel;
                    }
                }
            }
        }
        public RegisterViewModel GetUserRoleById(int id)
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
                            WHERE ur.userId = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        RegisterViewModel registerViewModel = null;
                        if (reader.Read())
                        {
                            registerViewModel = new RegisterViewModel()
                            {
                                UserRole = new UserRole()
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
                                    Role = Role.FromValue(DbUtils.GetInt(reader, "roleId")),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                }
                            };
                        }
                        return registerViewModel;
                    }
                }
            }
        }
        public void UpdateUserProfile(RegisterViewModel registerViewModel)
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
                                updatedAt = @updatedAt
                        WHERE Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@userId", registerViewModel.UserProfile.UserId);
                    DbUtils.AddParameter(cmd, "@profileImageUrl", registerViewModel.UserProfile.ProfileImageUrl);
                    DbUtils.AddParameter(cmd, "@firstName", registerViewModel.UserProfile.FirstName);
                    DbUtils.AddParameter(cmd, "@lastName", registerViewModel.UserProfile.LastName);
                    DbUtils.AddParameter(cmd, "@handle", registerViewModel.UserProfile.Handle);
                    DbUtils.AddParameter(cmd, "@pronounId", registerViewModel.UserProfile.PronoundId);
                    DbUtils.AddParameter(cmd, "@biography", registerViewModel.UserProfile.Biography);
                    DbUtils.AddParameter(cmd, "@biographyUrl", registerViewModel.UserProfile.BiographyUrl);
                    DbUtils.AddParameter(cmd, "@birthday", registerViewModel.UserProfile.Birthday);
                    DbUtils.AddParameter(cmd, "@createdAt", registerViewModel.UserProfile.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", registerViewModel.UserProfile.UpdatedAt);
                    DbUtils.AddParameter(cmd, "@id", registerViewModel.UserProfile.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void AddUserRole(RegisterViewModel registerViewModel)
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

                    DbUtils.AddParameter(cmd, "@userId", registerViewModel.UserRole.UserId);
                    DbUtils.AddParameter(cmd, "@roleId", registerViewModel.UserRole.Role.Value);
                    DbUtils.AddParameter(cmd, "@createdAt", registerViewModel.UserRole.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", registerViewModel.UserRole.UpdatedAt);

                    registerViewModel.UserRole.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
