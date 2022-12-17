using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using BookBurrow.ViewModels;
using CloudinaryDotNet.Actions;

namespace BookBurrow.Repositories
{
    public class LoginViewModelRepository : BaseRepository, ILoginViewModelRepository
    {
        public LoginViewModelRepository(IConfiguration configuration) : base(configuration) { }

        public List<LoginViewModel> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT u.id AS userId, u.firebaseUID, u.email, u.createdAt AS userCreatedAt, u.updatedAt AS userUpdatedAt,
                            up.id AS userProfileId, up.userId, up.profileImageUrl, up.firstName, up.lastName, up.handle, up.pronounId, 
                            up.biography, up.biographyUrl, up.birthday, up.createdAt AS userProfileCreatedAt, up.updatedAt AS userProfileUpdatedAt,
                            p.pronouns
                        FROM dbo.[User] u
                            JOIN dbo.UserProfile up ON u.id = up.userId
                            JOIN dbo.UserPronoun p ON up.pronounId = p.id
                        ORDER BY u.createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var usersAndProfiles = new List<LoginViewModel>();
                        while (reader.Read())
                        {
                            usersAndProfiles.Add(new LoginViewModel()
                            {
                                User = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "userId"),
                                    FirebaseUID = DbUtils.GetString(reader, "firebaseUID"),
                                    Email = DbUtils.GetString(reader, "email"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "userUpdatedAt"),
                                },
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "userProfileId"),
                                    UserId = DbUtils.GetInt(reader, "userId"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                    FirstName = DbUtils.GetString(reader, "firstName"),
                                    LastName = DbUtils.GetString(reader, "lastName"),
                                    Handle = DbUtils.GetString(reader, "handle"),
                                    PronounId = DbUtils.GetNullableInt(reader, "pronounId"),
                                    Biography = DbUtils.GetString(reader, "biography"),
                                    BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                    Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userProfileCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "userProfileUpdatedAt"),
                                },
                            });
                        }
                        return usersAndProfiles;
                    }
                }
            }
        }
        
        public void AddUser(LoginViewModel loginViewModel)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.[User] (firebaseUID, email, createdAt, updatedAt)
                        OUTPUT INSERTED.id
                        VALUES (@firebaseUID, @email, @createdAt, @updatedAt)
                    ";
                    DbUtils.AddParameter(cmd, "@firebaseUID", loginViewModel.User.FirebaseUID);
                    DbUtils.AddParameter(cmd, "@email", loginViewModel.User.Email);
                    DbUtils.AddParameter(cmd, "@createdAt", loginViewModel.User.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", loginViewModel.User.UpdatedAt);

                    loginViewModel.User.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void AddUserProfile(LoginViewModel loginViewModel)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.UserProfile (userId, profileImageUrl, firstName, lastName, handle, pronounId, biography, biographyUrl, birthday, createdAt, updatedAt)
                        OUTPUT INSERTED.id
                        VALUES (@userId, @profileImageUrl, @firstName, @lastName, @handle, @pronounId, @biography, @biographyUrl, @birthday, @createdAt, @updatedAt)
                    ";
                    DbUtils.AddParameter(cmd, "@userId", loginViewModel.UserProfile.UserId);
                    DbUtils.AddParameter(cmd, "@profileImageUrl", loginViewModel.UserProfile.ProfileImageUrl);
                    DbUtils.AddParameter(cmd, "@firstName", loginViewModel.UserProfile.FirstName);
                    DbUtils.AddParameter(cmd, "@lastName", loginViewModel.UserProfile.LastName);
                    DbUtils.AddParameter(cmd, "@handle", loginViewModel.UserProfile.Handle);
                    DbUtils.AddParameter(cmd, "@pronounId", loginViewModel.UserProfile.PronounId);
                    DbUtils.AddParameter(cmd, "@biography", loginViewModel.UserProfile.Biography);
                    DbUtils.AddParameter(cmd, "@biographyUrl", loginViewModel.UserProfile.BiographyUrl);
                    DbUtils.AddParameter(cmd, "@birthday", loginViewModel.UserProfile.Birthday);
                    DbUtils.AddParameter(cmd, "@createdAt", loginViewModel.UserProfile.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", loginViewModel.UserProfile.UpdatedAt);

                    loginViewModel.UserProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
