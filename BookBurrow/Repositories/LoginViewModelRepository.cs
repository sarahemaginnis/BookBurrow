using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace BookBurrow.Repositories
{
    public class LoginViewModelRepository : BaseRepository, ILoginViewModelRepository
    {
        public LoginViewModelRepository(IConfiguration configuration) : base(configuration) { }
        public void Add(User user)
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
                    DbUtils.AddParameter(cmd, "@firebaseUID", user.FirebaseUID);
                    DbUtils.AddParameter(cmd, "@email", user.Email);
                    DbUtils.AddParameter(cmd, "@createdAt", user.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", user.UpdatedAt);

                    user.Id = (int)cmd.ExecuteScalar();
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
                        VALUES (@userId, @profileImageUrl, @firstName, @lastName, @handle, @pronounId, @biography, @biographyUrl, @birthday, @createdAt, @updatedAt)
                    ";
                    DbUtils.AddParameter(cmd, "@userId", userProfile.UserId);
                    DbUtils.AddParameter(cmd, "@profileImageUrl", userProfile.ProfileImageUrl);
                    DbUtils.AddParameter(cmd, "@firstName", userProfile.FirstName);
                    DbUtils.AddParameter(cmd, "@lastName", userProfile.LastName);
                    DbUtils.AddParameter(cmd, "@handle", userProfile.Handle);
                    DbUtils.AddParameter(cmd, "@pronounId", userProfile.PronoundId);
                    DbUtils.AddParameter(cmd, "@biography", userProfile.Biography);
                    DbUtils.AddParameter(cmd, "@biographyUrl", userProfile.BiographyUrl);
                    DbUtils.AddParameter(cmd, "@birthday", userProfile.Birthday);
                    DbUtils.AddParameter(cmd, "@createdAt", userProfile.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", userProfile.UpdatedAt);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
