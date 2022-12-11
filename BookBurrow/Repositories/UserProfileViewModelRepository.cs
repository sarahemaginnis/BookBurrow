using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using BookBurrow.Models;
using BookBurrow.Utils;
using Microsoft.Data.SqlClient;
using BookBurrow.ViewModels;
using CloudinaryDotNet.Actions;
using System.Net;

namespace BookBurrow.Repositories
{
    public class UserProfileViewModelRepository : BaseRepository, IUserProfileViewModelRepository
    {
        public UserProfileViewModelRepository(IConfiguration configuration) : base(configuration) { }

        public UserProfileViewModel GetUserBiographicalSectionById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT u.id, u.firebaseUID, u.email, u.createdAt AS userCreatedAt, u.updatedAt AS userUpdatedAt,
                            up.id AS userProfileId, up.profileImageUrl, up.firstName, up.lastName, up.handle, up.pronounId,
                            p.pronouns,
                            up.biography, up.biographyUrl, up.birthday, up.createdAt AS userProfileCreatedAt, up.updatedAt AS userProfileUpdatedAt
                        FROM dbo.[User] u
                            JOIN dbo.UserProfile up ON u.id = up.userId
                            JOIN dbo.UserPronoun p ON up.pronounId = p.id
                        WHERE u.Id = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        UserProfileViewModel userProfileViewModel = null;
                        if (reader.Read())
                        {
                            userProfileViewModel = new UserProfileViewModel()
                            {
                                User = new User()
                                {
                                    Id = id,
                                    FirebaseUID = DbUtils.GetString(reader, "firebaseUID"),
                                    Email = DbUtils.GetString(reader, "email"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "userUpdatedAt"),
                                },
                                UserProfile = new UserProfile()
                                {
                                    Id = DbUtils.GetInt(reader, "userProfileId"),
                                    UserId = id,
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
                            };
                        }
                        return userProfileViewModel;
                    }
                }
            }
        }

        public List<UserProfileViewModel> GetAllUserPostsByUserIdForCount(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, userId, postTypeId, bookId, title, cloudinaryUrl, caption, source, songUrl, songUrlSummary, createdAt, updatedAt
                        FROM dbo.UserPost
                        WHERE userId = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfileViewModels = new List<UserProfileViewModel>();
                        while (reader.Read())
                        {
                            userProfileViewModels.Add(new UserProfileViewModel()
                            {
                                UserPost = new UserPost()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
                                    UserId = DbUtils.GetInt(reader, "userId"),
                                    PostType = PostType.FromValue(DbUtils.GetInt(reader, "postTypeId")),
                                    BookId = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "postTitle"),
                                    CloudinaryUrl = DbUtils.GetString(reader, "cloudinaryUrl"),
                                    Caption = DbUtils.GetString(reader, "postCaption"),
                                    Source = DbUtils.GetString(reader, "source"),
                                    SongUrl = DbUtils.GetString(reader, "songUrl"),
                                    SongUrlSummary = DbUtils.GetString(reader, "songUrlSummary"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                }
                            });
                        }
                        return userProfileViewModels;
                    }
                }
            }
        }

        public List<UserProfileViewModel> GetAllUserFollowersByUserIdForCount(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT u.firebaseUID, u.email, u.createdAt, u.updatedAt,
                            uf.id as userFollowerId, uf.userId, uf.followerId, uf.createdAt AS followStartDate
                        FROM dbo.[User] u
                            JOIN dbo.UserFollower uf ON u.id = uf.userId
                        WHERE uf.userId = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfileViewModels = new List<UserProfileViewModel>();
                        while (reader.Read())
                        {
                            userProfileViewModels.Add(new UserProfileViewModel()
                            {
                                User = new User()
                                {
                                    Id = id,
                                    FirebaseUID = DbUtils.GetString(reader, "firebaseUID"),
                                    Email = DbUtils.GetString(reader, "email"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "userUpdatedAt"),
                                }
                            });
                        }
                        return userProfileViewModels;
                    }
                }
            }
        }

        public List<UserProfileViewModel> GetAllUserFollowingByUserIdForCount(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT u.id, u.firebaseUID, u.email, u.createdAt, u.updatedAt,
                            uf.id as userFollowerId, uf.userId, uf.followerId, uf.createdAt AS followStartDate
                        FROM dbo.[User] u
                            JOIN dbo.UserFollower uf ON u.id = uf.followerId
                        WHERE uf.followerId = @id
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfileViewModels = new List<UserProfileViewModel>();
                        while (reader.Read())
                        {
                            userProfileViewModels.Add(new UserProfileViewModel()
                            {
                                User = new User()
                                {
                                    Id = id,
                                    FirebaseUID = DbUtils.GetString(reader, "firebaseUID"),
                                    Email = DbUtils.GetString(reader, "email"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "userCreatedAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "userUpdatedAt"),
                                }
                            });
                        }
                        return userProfileViewModels;
                    }
                }
            }
        }

        public List<UserProfileViewModel> GetAllUserBooksCurrentlyReadingByUserIdOrderedByStartReadingDateDescending(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ub.id, ub.bookId, ub.userId, ub.startDate, ub.endDate, ub.ratingId, ub.statusId, ub.review, ub.reviewCreatedAt, ub.reviewEditedAt,
                            b.title, b.isbn, b.description, b.coverImageUrl, b.datePublished, b.createdAt AS bookRecordCreated, b.updatedAt AS bookRecordUpdated,
                            a.id AS authorId, a.userId AS authorUserId, a.firstName AS authorFirstName, a.middleName AS authorMiddleName, a.lastName AS authorLastName, a.profileImageUrl
                        FROM dbo.UserBook ub
                            JOIN dbo.Book b ON ub.bookId = b.id
                            JOIN dbo.BookAuthor ba ON b.id = ba.bookId
                            JOIN dbo.Author a ON ba.authorId = a.id
                        WHERE ub.userId = @id
                        ORDER BY startDate DESC
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfileViewModels = new List<UserProfileViewModel>();
                        while (reader.Read())
                        {
                            userProfileViewModels.Add(new UserProfileViewModel()
                            {
                                Book = new Book()
                                {
                                    Id = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "title"),
                                    Isbn = DbUtils.GetString(reader, "isbn"),
                                    Description = DbUtils.GetString(reader, "description"),
                                    CoverImageUrl = DbUtils.GetString(reader, "coverImageUrl"),
                                    DatePublished = DbUtils.GetDateTime(reader, "datePublished"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "bookRecordCreated"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "bookRecordUpdated")
                                },
                                Author = new Author()
                                {
                                    Id = DbUtils.GetInt(reader, "authorId"),
                                    UserId = DbUtils.GetNullableInt(reader, "authorUserId"),
                                    FirstName = DbUtils.GetString(reader, "authorFirstName"),
                                    MiddleName = DbUtils.GetString(reader, "authorMiddleName"),
                                    LastName = DbUtils.GetString(reader, "authorLastName"),
                                    ProfileImageUrl = DbUtils.GetString(reader, "profileImageUrl")
                                }
                            });
                        }
                        return userProfileViewModels;
                    }
                }
            }
        }

        public List<UserProfileViewModel> GetAllUserPostsByUserIdOrderedByCreationDateDescending(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT id, userId, postTypeId, bookId, title, cloudinaryUrl, caption, source, songUrl, songUrlSummary, createdAt, updatedAt
                        FROM dbo.UserPost
                        WHERE userId = @id
                        ORDER BY createdAt DESC
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfileViewModels = new List<UserProfileViewModel>();
                        while (reader.Read())
                        {
                            userProfileViewModels.Add(new UserProfileViewModel()
                            {
                                UserPost = new UserPost()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
                                    UserId = DbUtils.GetInt(reader, "userId"),
                                    PostType = PostType.FromValue(DbUtils.GetInt(reader, "postTypeId")),
                                    BookId = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "title"),
                                    CloudinaryUrl = DbUtils.GetString(reader, "cloudinaryUrl"),
                                    Caption = DbUtils.GetString(reader, "caption"),
                                    Source = DbUtils.GetString(reader, "source"),
                                    SongUrl = DbUtils.GetString(reader, "songUrl"),
                                    SongUrlSummary = DbUtils.GetString(reader, "songUrlSummary"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                }
                            });
                        }
                        return userProfileViewModels;
                    }
                }
            }
        }

        public List<UserProfileViewModel> GetAllFavoritedUserPostsByUserIdOrderedByFavoritedDateDescending(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.id, up.userId AS PostCreatorId, up.postTypeId, up.bookId, up.title, up.cloudinaryUrl, up.caption, up.source, 
                            up.songUrl, up.songUrlSummary, up.createdAt, up.updatedAt,
                            pf.id AS postFavoriteId, pf.userId, pf.postId, pf.createdAt AS postFavoritedAt
                        FROM dbo.UserPost up
                            JOIN dbo.PostFavorite pf ON up.id = pf.postId
                        WHERE pf.userId = @id
                        ORDER BY postFavoritedAt DESC
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfileViewModels = new List<UserProfileViewModel>();
                        while (reader.Read())
                        {
                            userProfileViewModels.Add(new UserProfileViewModel()
                            {
                                UserPost = new UserPost()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
                                    UserId = DbUtils.GetInt(reader, "postCreatorId"),
                                    PostType = PostType.FromValue(DbUtils.GetInt(reader, "postTypeId")),
                                    BookId = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "title"),
                                    CloudinaryUrl = DbUtils.GetString(reader, "cloudinaryUrl"),
                                    Caption = DbUtils.GetString(reader, "caption"),
                                    Source = DbUtils.GetString(reader, "source"),
                                    SongUrl = DbUtils.GetString(reader, "songUrl"),
                                    SongUrlSummary = DbUtils.GetString(reader, "songUrlSummary"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                },
                            });
                        }
                        return userProfileViewModels;
                    }
                }
            }
        }

        public List<UserProfileViewModel> GetAllLikedUserPostsByUserIdOrderedByLikedDateDescending(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT up.id, up.userId AS PostCreatorId, up.postTypeId, up.bookId, up.title, up.cloudinaryUrl, up.caption, up.source, 
                            up.songUrl, up.songUrlSummary, up.createdAt, up.updatedAt,
                            pl.id AS postLikedId, pl.userId, pl.postId, pl.createdAt AS postLikedAt
                        FROM dbo.UserPost up
                            JOIN dbo.PostLike pl ON up.id = pl.postId
                        WHERE pl.userId = @id
                        ORDER BY postLikedAt DESC
                    ";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var userProfileViewModels = new List<UserProfileViewModel>();
                        while (reader.Read())
                        {
                            userProfileViewModels.Add(new UserProfileViewModel()
                            {
                                UserPost = new UserPost()
                                {
                                    Id = DbUtils.GetInt(reader, "id"),
                                    UserId = DbUtils.GetInt(reader, "postCreatorId"),
                                    PostType = PostType.FromValue(DbUtils.GetInt(reader, "postTypeId")),
                                    BookId = DbUtils.GetInt(reader, "bookId"),
                                    Title = DbUtils.GetString(reader, "title"),
                                    CloudinaryUrl = DbUtils.GetString(reader, "cloudinaryUrl"),
                                    Caption = DbUtils.GetString(reader, "caption"),
                                    Source = DbUtils.GetString(reader, "source"),
                                    SongUrl = DbUtils.GetString(reader, "songUrl"),
                                    SongUrlSummary = DbUtils.GetString(reader, "songUrlSummary"),
                                    CreatedAt = DbUtils.GetDateTime(reader, "createdAt"),
                                    UpdatedAt = DbUtils.GetDateTime(reader, "updatedAt"),
                                },
                            });
                        }
                        return userProfileViewModels;
                    }
                }
            }
        }
    }
}
