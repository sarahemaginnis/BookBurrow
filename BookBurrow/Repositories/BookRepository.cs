using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {

        public BookRepository(IConfiguration configuration) : base(configuration) { }

        public List<Book> GetAllOrderedByDatePublished()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, title, isbn, description, coverImageUrl, datePublished, createdAt, updatedAt
                            FROM dbo.Book
                        ORDER BY datePublished
                    ";
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var books = new List<Book>();
                        while (reader.Read())
                        {
                            books.Add(new Book()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                Title = DbUtils.GetString(reader, "title"),
                                Isbn = DbUtils.GetString(reader, "isbn"),
                                Description = DbUtils.GetString(reader, "description"),
                                CoverImageUrl = DbUtils.GetString(reader, "coverImageUrl"),
                                DatePublished = DbUtils.GetDateTime(reader, "datePublished"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            });
                        }
                        return books;
                    }
                }
            }
        }

        public Book GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT title, isbn, description, coverImageUrl, datePublished, createdAt, updatedAt
                            FROM dbo.Book
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Book book = null;
                        if (reader.Read())
                        {
                            book = new Book()
                            {
                                Id = id,
                                Title = DbUtils.GetString(reader, "title"),
                                Isbn = DbUtils.GetString(reader, "isbn"),
                                Description = DbUtils.GetString(reader, "description"),
                                CoverImageUrl = DbUtils.GetString(reader, "coverImageUrl"),
                                DatePublished = DbUtils.GetDateTime(reader, "datePublished"),
                                CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                            };
                        }
                        return book;
                    }
                }
            }
        }

        public void Add(Book book)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.Book (title, isbn, description, coverImageUrl, datePublished, createdAt, updatedAt)
                        OUTPUT INSERTED.id
                        VALUES (@title, @isbn, @description, @coverImageUrl, @datePublished, @createdAt, @updatedAt)
                    ";

                    DbUtils.AddParameter(cmd, "@title", book.Title);
                    DbUtils.AddParameter(cmd, "@isbn", book.Isbn);
                    DbUtils.AddParameter(cmd, "@description", book.Description);
                    DbUtils.AddParameter(cmd, "@coverImageUrl", book.CoverImageUrl);
                    DbUtils.AddParameter(cmd, "@datePublished", book.DatePublished);
                    DbUtils.AddParameter(cmd, "@createdAt", book.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", book.UpdatedAt);

                    book.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Book book)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.Book
                            SET title = @title,
                                isbn = @isbn,
                                description = @description,
                                coverImageUrl = @coverImageUrl,
                                datePublished = @datePublished,
                                createdAt = @createdAt,
                                updatedAt = @updatedAt
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@title", book.Title);
                    DbUtils.AddParameter(cmd, "@isbn", book.Isbn);
                    DbUtils.AddParameter(cmd, "@description", book.Description);
                    DbUtils.AddParameter(cmd, "@coverImageUrl", book.CoverImageUrl);
                    DbUtils.AddParameter(cmd, "@datePublished", book.DatePublished);
                    DbUtils.AddParameter(cmd, "@createdAt", book.CreatedAt);
                    DbUtils.AddParameter(cmd, "@updatedAt", book.UpdatedAt);
                    DbUtils.AddParameter(cmd, "@id", book.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.Book WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
