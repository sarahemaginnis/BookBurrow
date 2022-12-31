using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class BookAuthorRepository : BaseRepository, IBookAuthorRepository
    {
        public BookAuthorRepository(IConfiguration configuration) : base(configuration) { }
        public List<BookAuthor> GetAllOrderedByTitle()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ba.id, ba.bookId, ba.authorId,
                                b.title, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt, b.updatedAt,
                                a.userId, a.firstName, a.middleName, a.lastName, a.profileImageUrl
                            FROM dbo.BookAuthor ba
                                JOIN dbo.Book b ON ba.bookId = b.id
                                JOIN dbo.Author a ON ba.authorId = a.id
                        ORDER BY title
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var bookAuthors = new List<BookAuthor>();
                        while (reader.Read())
                        {
                            bookAuthors.Add(new BookAuthor()
                            {
                                Id = DbUtils.GetInt(reader, "id"),
                                BookId = DbUtils.GetInt(reader, "bookId"),
                                Book = new Book()
                                {
                                    Id = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "title"),
                                    Isbn = DbUtils.GetString(reader, "isbn"),
                                    Description = DbUtils.GetString(reader, "description"),
                                    CoverImageUrl = DbUtils.GetString(reader, "coverImageUrl"),
                                    DatePublished = DbUtils.GetDateTime(reader, "datePublished"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                },
                                AuthorId = DbUtils.GetInt(reader, "authorId"),
                                Author = new Author()
                                {
                                    Id = DbUtils.GetInt(reader, "authorId"),
                                    UserId = DbUtils.GetNullableInt(reader, "userId"),
                                    FirstName = DbUtils.GetString(reader, "firstName"),
                                    MiddleName = DbUtils.GetString(reader, "middleName"),
                                    LastName = DbUtils.GetString(reader, "lastName"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                },
                            });
                        }
                        return bookAuthors;
                    }
                }
            }
        }

        public List<BookAuthor> Search(string criterion)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    var sql = @"
                        SELECT b.id AS bookId, b.title, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt, b.updatedAt,
                                a.id AS authorId, a.userId, a.firstName, a.middleName, a.lastName, a.profileImageUrl
                            FROM dbo.Book b
                                LEFT JOIN dbo.BookAuthor ba ON b.id = ba.bookId
                                LEFT JOIN dbo.Author a ON ba.authorId = a.id
                        WHERE b.title LIKE @criterion OR b.description LIKE @criterion OR a.firstName LIKE @criterion OR a.middleName LIKE @criterion 
                            OR a.lastName LIKE @criterion
                    ";

                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@criterion", criterion + "%");
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var books = new List<BookAuthor>();
                        while (reader.Read())
                        {
                            books.Add(new BookAuthor()
                            {
                                BookId = DbUtils.GetInt(reader, "bookId"),
                                Book = new Book()
                                {
                                    Id = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "title"),
                                    Isbn = DbUtils.GetString(reader, "isbn"),
                                    Description = DbUtils.GetString(reader, "description"),
                                    CoverImageUrl = DbUtils.GetString(reader, "coverImageUrl"),
                                    DatePublished = DbUtils.GetDateTime(reader, "datePublished"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                },
                                AuthorId = DbUtils.GetInt(reader, "authorId"),
                                Author = new Author()
                                {
                                    Id = DbUtils.GetInt(reader, "authorId"),
                                    UserId = DbUtils.GetNullableInt(reader, "userId"),
                                    FirstName = DbUtils.GetString(reader, "firstName"),
                                    MiddleName = DbUtils.GetString(reader, "middleName"),
                                    LastName = DbUtils.GetString(reader, "lastName"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                },
                            });
                        }
                        return books;
                    }
                }
            }
        }

        public BookAuthor GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ba.bookId, ba.authorId,
                                b.title, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt, b.updatedAt,
                                a.userId, a.firstName, a.middleName, a.lastName, a.profileImageUrl
                            FROM dbo.BookAuthor ba
                                JOIN dbo.Book b ON ba.bookId = b.id
                                JOIN dbo.Author a ON ba.authorId = a.id
                        WHERE ba.Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        BookAuthor bookAuthor = null;
                        if (reader.Read())
                        {
                            bookAuthor = new BookAuthor()
                            {
                                Id = id,
                                BookId = DbUtils.GetInt(reader, "bookId"),
                                Book = new Book()
                                {
                                    Id = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "title"),
                                    Isbn = DbUtils.GetString(reader, "isbn"),
                                    Description = DbUtils.GetString(reader, "description"),
                                    CoverImageUrl = DbUtils.GetString(reader, "coverImageUrl"),
                                    DatePublished = DbUtils.GetDateTime(reader, "datePublished"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                },
                                AuthorId = DbUtils.GetInt(reader, "authorId"),
                                Author = new Author()
                                {
                                    Id = DbUtils.GetInt(reader, "authorId"),
                                    UserId = DbUtils.GetNullableInt(reader, "userId"),
                                    FirstName = DbUtils.GetString(reader, "firstName"),
                                    MiddleName = DbUtils.GetString(reader, "middleName"),
                                    LastName = DbUtils.GetString(reader, "lastName"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                },
                            };
                        }
                        return bookAuthor;
                    }
                }
            }
        }

        public BookAuthor GetByBookId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ba.id, ba.bookId, ba.authorId,
                                b.title, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt, b.updatedAt,
                                a.userId, a.firstName, a.middleName, a.lastName, a.profileImageUrl
                            FROM dbo.BookAuthor ba
                                JOIN dbo.Book b ON ba.bookId = b.id
                                JOIN dbo.Author a ON ba.authorId = a.id
                        WHERE ba.bookId = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        BookAuthor bookAuthor = null;
                        if (reader.Read())
                        {
                            bookAuthor = new BookAuthor()
                            {
                                Id = DbUtils.GetInt(reader, "bookId"),
                                BookId = id,
                                Book = new Book()
                                {
                                    Id = id,
                                    Title = DbUtils.GetString(reader, "title"),
                                    Isbn = DbUtils.GetString(reader, "isbn"),
                                    Description = DbUtils.GetString(reader, "description"),
                                    CoverImageUrl = DbUtils.GetString(reader, "coverImageUrl"),
                                    DatePublished = DbUtils.GetDateTime(reader, "datePublished"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                },
                                AuthorId = DbUtils.GetInt(reader, "authorId"),
                                Author = new Author()
                                {
                                    Id = DbUtils.GetInt(reader, "authorId"),
                                    UserId = DbUtils.GetNullableInt(reader, "userId"),
                                    FirstName = DbUtils.GetString(reader, "firstName"),
                                    MiddleName = DbUtils.GetString(reader, "middleName"),
                                    LastName = DbUtils.GetString(reader, "lastName"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl"),
                                },
                            };
                        }
                        return bookAuthor;
                    }
                }
            }
        }

        public void Add(BookAuthor bookAuthor)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.BookAuthor (bookId, authorId)
                        OUTPUT INSERTED.id
                        VALUES (@bookId, @authorId)
                    ";

                    DbUtils.AddParameter(cmd, "@bookId", bookAuthor.BookId);
                    DbUtils.AddParameter(cmd, "@authorId", bookAuthor.AuthorId);
                    bookAuthor.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(BookAuthor bookAuthor)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.BookAuthor
                            SET bookId = @bookId,
                                authorId = @authorId
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@bookId", bookAuthor.BookId);
                    DbUtils.AddParameter(cmd, "@authorId", bookAuthor.AuthorId);
                    DbUtils.AddParameter(cmd, "@id", bookAuthor.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.BookAuthor WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
