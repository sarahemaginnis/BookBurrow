using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;

namespace BookBurrow.Repositories
{
    public class SeriesBookRepository : BaseRepository, ISeriesBookRepository
    {
        public SeriesBookRepository(IConfiguration configuration) : base(configuration) { }
        public List<SeriesBook> GetAllOrderedBySeriesPosition()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT sb.id, sb.bookId, sb.seriesId, sb.position,
                                b.title, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt, b.updatedAt,
                                s.name
                            FROM dbo.SeriesBook sb
                                JOIN dbo.Book b ON sb.bookId = b.id
                                JOIN dbo.Series s ON sb.seriesId = s.id
                        ORDER BY seriesId, position
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var seriesBooks = new List<SeriesBook>();
                        while (reader.Read())
                        {
                            seriesBooks.Add(new SeriesBook()
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
                                SeriesId = DbUtils.GetInt(reader, "seriesId"),
                                Series = new Series()
                                {
                                    Id = DbUtils.GetInt(reader, "seriesId"),
                                    Name = DbUtils.GetString(reader, "name"),
                                },
                                Position = DbUtils.GetInt(reader, "position"),
                            });
                        }
                        return seriesBooks;
                    }
                }
            }
        }

        public SeriesBook GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT sb.bookId, sb.seriesId, sb.position,
                                b.title, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt, b.updatedAt,
                                s.name
                            FROM dbo.SeriesBook sb
                                JOIN dbo.Book b ON sb.bookId = b.id
                                JOIN dbo.Series s ON sb.seriesId = s.id
                        WHERE sb.Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        SeriesBook seriesBook = null;
                        if (reader.Read())
                        {
                            seriesBook = new SeriesBook()
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
                                SeriesId = DbUtils.GetInt(reader, "seriesId"),
                                Series = new Series()
                                {
                                    Id = DbUtils.GetInt(reader, "seriesId"),
                                    Name = DbUtils.GetString(reader, "name"),
                                },
                                Position = DbUtils.GetInt(reader, "position"),
                            };
                        }
                        return seriesBook;
                    }
                }
            }
        }

        public void Add(SeriesBook seriesBook)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.SeriesBook (bookId, seriesId, position)
                        OUTPUT INSERTED.id
                        VALUES (@bookId, @seriesId, @position)
                    ";

                    DbUtils.AddParameter(cmd, "@bookId", seriesBook.BookId);
                    DbUtils.AddParameter(cmd, "@seriesId", seriesBook.SeriesId);
                    DbUtils.AddParameter(cmd, "@position", seriesBook.Position);

                    seriesBook.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(SeriesBook seriesBook)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.SeriesBook
                            SET bookId = @bookId,
                                seriesId = @seriesId,
                                position = @position
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@bookId", seriesBook.BookId);
                    DbUtils.AddParameter(cmd, "@seriesId", seriesBook.SeriesId);
                    DbUtils.AddParameter(cmd, "@position", seriesBook.Position);
                    DbUtils.AddParameter(cmd, "@id", seriesBook.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.SeriesBook WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
