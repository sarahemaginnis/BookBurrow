using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace BookBurrow.Repositories
{
    public class UserBookRepository : BaseRepository, IUserBookRepository
    {
        public UserBookRepository(IConfiguration configuration) : base(configuration) { }
        public List<UserBook> GetAllOrderedByReviewCreatedAt()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ub.id, ub.bookId,
                            b.title, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt AS bookRecordCreatedAt, 
                            b.updatedAt AS bookRecordUpdatedAt,
                            ub.startDate, ub.endDate,
                            ub.ratingId, r.displayValue AS rating,
                            ub.statusId, 
                            ub.review, ub.reviewCreatedAt, ub.reviewEditedAt,
                            ub.userId,
                            up.id AS userProfileId, up.profileImageUrl, up.firstName, up.lastName, up.handle, up.pronounId, p.pronouns, 
                            up.biography, up.biographyUrl, up.birthday, up.createdAt AS userProfileCreatedAt, up.updatedAt AS userProfileUpdatedAt
                            FROM dbo.UserBook ub
                                JOIN dbo.Book b ON ub.bookId = b.id
                                JOIN dbo.UserProfile up ON ub.userId = up.userId
                                JOIN dbo.Rating r ON ub.ratingId = r.id
                                JOIN dbo.UserPronoun p ON up.pronounId = p.id
                        ORDER BY reviewCreatedAt
                    ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userBooks = new List<UserBook>();
                        while (reader.Read())
                        {
                            userBooks.Add(new UserBook()
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
                                    CreatedAt = DbUtils.GetDateTime(reader, "bookRecordCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "bookRecordUpdatedAt"),
                                },
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
                                    Biography = DbUtils.GetString(reader, "biography"),
                                    BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                    Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userProfileCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "userProfileUpdatedAt"),
                                },
                                UserPronoun = new UserPronoun()
                                {
                                    Id = DbUtils.GetInt(reader, "pronounId"),
                                    Pronouns = DbUtils.GetString(reader, "pronouns"),
                                },
                                StartDate = DbUtils.GetNullableDateTime(reader, "startDate"),
                                EndDate = DbUtils.GetNullableDateTime(reader, "endDate"),
                                RatingId = DbUtils.GetNullableInt(reader, "ratingId"),
                                Rating = new Rating()
                                {
                                    Id = DbUtils.GetInt(reader, "ratingId"),
                                    DisplayValue = reader.GetDecimal(reader.GetOrdinal("rating")),
                                },
                                BookStatus = BookStatus.FromValue(DbUtils.GetInt(reader, "statusId")),
                                Review = DbUtils.GetString(reader, "review"),
                                ReviewCreatedAt = DbUtils.GetDateTime(reader, "reviewCreatedAt"),
                                ReviewEditedAt = DbUtils.GetDateTime(reader, "reviewEditedAt"),
                            });
                        }
                        return userBooks;
                    }
                }
            }
        }

        public UserBook GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT ub.bookId,
                            b.title, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt AS bookRecordCreatedAt, 
                            b.updatedAt AS bookRecordUpdatedAt,
                            ub.startDate, ub.endDate,
                            ub.ratingId, r.displayValue AS rating,
                            ub.statusId, 
                            ub.review, ub.reviewCreatedAt, ub.reviewEditedAt,
                            ub.userId,
                            up.id AS userProfileId, up.profileImageUrl, up.firstName, up.lastName, up.handle, up.pronounId, p.pronouns, 
                            up.biography, up.biographyUrl, up.birthday, up.createdAt AS userProfileCreatedAt, up.updatedAt AS userProfileUpdatedAt
                            FROM dbo.UserBook ub
                                JOIN dbo.Book b ON ub.bookId = b.id
                                JOIN dbo.UserProfile up ON ub.userId = up.userId
                                JOIN dbo.Rating r ON ub.ratingId = r.id
                                JOIN dbo.UserPronoun p ON up.pronounId = p.id
                    WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserBook userBook = null;
                        if (reader.Read())
                        {
                            userBook = new UserBook()
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
                                    CreatedAt = DbUtils.GetDateTime(reader, "bookRecordCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "bookRecordUpdatedAt"),
                                },
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
                                    Biography = DbUtils.GetString(reader, "biography"),
                                    BiographyUrl = DbUtils.GetString(reader, "biographyUrl"),
                                    Birthday = DbUtils.GetDateTime(reader, "birthday"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userProfileCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "userProfileUpdatedAt"),
                                },
                                UserPronoun = new UserPronoun()
                                {
                                    Id = DbUtils.GetInt(reader, "pronounId"),
                                    Pronouns = DbUtils.GetString(reader, "pronouns"),
                                },
                                StartDate = DbUtils.GetNullableDateTime(reader, "startDate"),
                                EndDate = DbUtils.GetNullableDateTime(reader, "endDate"),
                                RatingId = DbUtils.GetNullableInt(reader, "ratingId"),
                                Rating = new Rating()
                                {
                                    Id = DbUtils.GetInt(reader, "ratingId"),
                                    DisplayValue = reader.GetDecimal(reader.GetOrdinal("rating")),
                                },
                                BookStatus = BookStatus.FromValue(DbUtils.GetInt(reader, "statusId")),
                                Review = DbUtils.GetString(reader, "review"),
                                ReviewCreatedAt = DbUtils.GetDateTime(reader, "reviewCreatedAt"),
                                ReviewEditedAt = DbUtils.GetDateTime(reader, "reviewEditedAt"),
                            };
                        }
                        return userBook;
                    }
                }
            }
        }

        public void Add(UserBook userBook)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO dbo.UserBook (bookId, userId, startDate, endDate, ratingId, statusId, review, reviewCreatedAt, reviewEditedAt)
                        OUTPUT INSERTED.id
                        VALUES (@bookId, @userId, @startDate, @endDate, @ratingId, @statusId, @review, @reviewCreatedAt, @reviewEditedAt)
                    ";

                    DbUtils.AddParameter(cmd, "@bookId", userBook.BookId);
                    DbUtils.AddParameter(cmd, "@userId", userBook.UserId);
                    DbUtils.AddParameter(cmd, "@startDate", userBook.StartDate);
                    DbUtils.AddParameter(cmd, "@endDate", userBook.EndDate);
                    DbUtils.AddParameter(cmd, "@ratingId", userBook.RatingId);
                    DbUtils.AddParameter(cmd, "@statusId", userBook.BookStatus.Value);
                    DbUtils.AddParameter(cmd, "@review", userBook.Rating);
                    DbUtils.AddParameter(cmd, "@reviewCreatedAt", userBook.ReviewCreatedAt);
                    DbUtils.AddParameter(cmd, "@reviewEditedAt", userBook.ReviewEditedAt);

                    userBook.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(UserBook userBook)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.UserBook
                            SET bookId = @bookId, 
                                userId = @userId, 
                                startDate = @startDate, 
                                endDate = @endDate, 
                                ratingId = @ratingId, 
                                statusId = @statusId, 
                                review = @review, 
                                reviewCreatedAt = @reviewCreatedAt, 
                                reviewEditedAt = @reviewEditedAt
                        WHERE Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@bookId", userBook.BookId);
                    DbUtils.AddParameter(cmd, "@userId", userBook.UserId);
                    DbUtils.AddParameter(cmd, "@startDate", userBook.StartDate);
                    DbUtils.AddParameter(cmd, "@endDate", userBook.EndDate);
                    DbUtils.AddParameter(cmd, "@ratingId", userBook.RatingId);
                    DbUtils.AddParameter(cmd, "@statusId", userBook.BookStatus.Value);
                    DbUtils.AddParameter(cmd, "@review", userBook.Rating);
                    DbUtils.AddParameter(cmd, "@reviewCreatedAt", userBook.ReviewCreatedAt);
                    DbUtils.AddParameter(cmd, "@reviewEditedAt", userBook.ReviewEditedAt);
                    DbUtils.AddParameter(cmd, "@id", userBook.Id);

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
                    cmd.CommandText = "DELETE FROM dbo.UserBook WHERE Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
