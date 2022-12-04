using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(IConfiguration configuration) : base(configuration) { }

        public List<Author> GetAllOrderedByLastName()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, userId, firstName, middleName, lastName, profileImageUrl
                        FROM dbo.Author
                        ORDER BY lastName
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var authors = new List<Author>();
                        while (reader.Read())
                        {
                            authors.Add(new Author()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                UserId = DbUtils.GetNullableInt(reader, "userId"),
                                FirstName = DbUtils.GetString(reader, "firstName"),
                                MiddleName = DbUtils.GetString(reader, "middleName"),
                                LastName = DbUtils.GetString(reader, "lastName"),
                                ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                            });
                        }

                        return authors;
                    }
                }
            }
        }

        public Author GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT userId, firstName, middleName, lastName, profileImageUrl
                        FROM dbo.Author
                        WHERE Id = @id
                    ";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Author author = null;
                        if (reader.Read())
                        {
                            author = new Author()
                            {
                                Id = id,
                                UserId = DbUtils.GetNullableInt(reader, "userId"),
                                FirstName = DbUtils.GetString(reader, "firstName"),
                                MiddleName = DbUtils.GetString(reader, "middleName"),
                                LastName = DbUtils.GetString(reader, "lastName"),
                                ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                            };
                        }
                        return author;
                    }
                }
            }
        }

        public void Add(Author author)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.Author (userId, firstName, middleName, lastName, profileImageUrl)
                        OUTPUT INSERTED.id
                        VALUES (@userId, @firstName, @middleName, @lastName, @profileImageUrl)
                    ";

                    DbUtils.AddParameter(cmd, "@userId", author.UserId);
                    DbUtils.AddParameter(cmd, "@firstName", author.FirstName);
                    DbUtils.AddParameter(cmd, "@middleName", author.MiddleName);
                    DbUtils.AddParameter(cmd, "@lastName", author.LastName);
                    DbUtils.AddParameter(cmd, "@profileImageUrl", author.ProfileImageUrl);

                    author.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Author author)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.Author
                            SET userId = @userId,
                                firstName = @firstName,
                                middleName = @middleName,
                                lastName = @lastName,
                                profileImageUrl = @profileImageUrl
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@userId", author.UserId);
                    DbUtils.AddParameter(cmd, "@firstName", author.FirstName);
                    DbUtils.AddParameter(cmd, "@middleName", author.MiddleName);
                    DbUtils.AddParameter(cmd, "@lastName", author.LastName);
                    DbUtils.AddParameter(cmd, "@profileImageUrl", author.ProfileImageUrl);
                    DbUtils.AddParameter(cmd, "@id", author.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.Author WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
