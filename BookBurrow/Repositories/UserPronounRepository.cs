using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class UserPronounRepository : BaseRepository, IUserPronounRepository
    {
        public UserPronounRepository(IConfiguration configuration) : base(configuration) { }

        public List<UserPronoun> GetAll()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, pronouns
                        FROM dbo.UserPronoun
                        ORDER BY id
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userPronouns = new List<UserPronoun>();
                        while (reader.Read())
                        {
                            userPronouns.Add(new UserPronoun()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                Pronouns = DbUtils.GetString(reader, "pronouns"),
                            });
                        }
                        return userPronouns;
                    }
                }
            }
        }

        public UserPronoun GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, pronouns
                        FROM dbo.UserPronoun
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserPronoun userPronoun = null;
                        if (reader.Read())
                        {
                            userPronoun = new UserPronoun()
                            {
                                Id = id,
                                Pronouns = DbUtils.GetString(reader, "pronouns"),
                            };
                        }
                        return userPronoun;
                    }
                }
            }
        }

        public void Add(UserPronoun userPronoun)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.UserPronoun (pronouns)
                        OUTPUT INSERTED.id
                        VALUES (@pronouns)
                    ";
                    DbUtils.AddParameter(cmd, "@pronouns", userPronoun.Pronouns);
                    userPronoun.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserPronoun userPronoun)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.UserPronoun
                            SET pronouns = @pronouns
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@pronouns", userPronoun.Pronouns);
                    DbUtils.AddParameter(cmd, "@id", userPronoun.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.UserPronoun WHERE id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
