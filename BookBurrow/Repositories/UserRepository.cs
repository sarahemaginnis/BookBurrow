using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }
        public List<User> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, firebaseUID, email, createdAt, updatedAt
                        FROM dbo.[User]
                        ORDER BY createdAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var users = new List<User>();
                        while (reader.Read())
                        {
                            users.Add(new User()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                FirebaseUID = DbUtils.GetString(reader, "firebaseUID"),
                                Email = DbUtils.GetString(reader, "email"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            });
                        }
                        return users;
                    }
                }
            }
        }

        public User GetById(int id)
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
                        User user = null;
                        if (reader.Read())
                        {
                            user = new User()
                            {
                                Id = id,
                                FirebaseUID = DbUtils.GetString(reader, "firebaseUID"),
                                Email = DbUtils.GetString(reader, "email"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            };
                        }
                        return user;
                    }
                }
            }
        }

        public User GetByFirebaseId(string id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, email, createdAt, updatedAt
                        FROM dbo.[User]
                        WHERE firebaseUID = @firebaseUID
                    ";
                    DbUtils.AddParameter(cmd, "@firebaseUID", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        User user = null;
                        if (reader.Read())
                        {
                            user = new User()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                FirebaseUID = id,
                                Email = DbUtils.GetString(reader, "email"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            };
                        }
                        return user;
                    }
                }
            }
        }

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

        public void Update(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.[User]
                        SET firebaseUID = @firebaseUID,
                            email = @email,
                            createdAt = @createdAt,
                            updatedAt = @updatedAt
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@firebaseUID", user.FirebaseUID);
                    DbUtils.AddParameter(cmd, "@email", user.Email);
                    DbUtils.AddParameter(cmd, "@createdAt", user.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", user.UpdatedAt);
                    DbUtils.AddParameter(cmd, "@id", user.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.[User] WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
