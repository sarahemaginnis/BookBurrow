USE [master]
GO

IF db_id('BookBurrow') IS NOT NULL
BEGIN
  ALTER DATABASE [BookBurrow] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
  DROP DATABASE [BookBurrow]
END
GO

CREATE DATABASE [BookBurrow];
GO

USE [BookBurrow];
GO

---------------------------------------------------------------------------------

CREATE TABLE [Users] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [firebaseUID] nvarchar(255),
  [email] nvarchar(255),
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [UserProfiles] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [profileImageUrl] varchar(254) NULL,
  [firstName] varchar(40) NULL,
  [lastName] varchar(40) NULL,
  [handle] varchar(40),
  [pronounId] int NULL,
  [biography] nvarchar(255) NULL,
  [biographyUrl] varchar(254) NULL,
  [birthday] datetime,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [UserPronouns] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [pronouns] varchar(40)
)
GO

CREATE TABLE [UserRoles] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [roleId] int,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [Roles] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [roleName] varchar(254),
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [Permissions] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [permissionName] varchar(254),
  [roleId] int,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [UserFollowers] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [followerId] int,
  [createdAt] datetime
)
GO

CREATE TABLE [Books] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [title] nvarchar(255),
  [firstAuthorId] int,
  [secondAuthorId] int NULL,
  [thirdAuthorId] int NULL,
  [isbn] nvarchar(255),
  [description] nvarchar(255) NULL,
  [coverImageUrl] varchar(254) NULL,
  [datePublished] datetime,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [Series] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [bookId] int,
  [collectionId] int,
  [positionId] int
)
GO

CREATE TABLE [SeriesCollection] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255)
)
GO

CREATE TABLE [SeriesPosition] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [position] int
)
GO

CREATE TABLE [Ratings] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [displayValue] decimal
)
GO

CREATE TABLE [Authors] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int NULL,
  [firstName] varchar(40) NULL,
  [middleName] varchar(40) NULL,
  [lastName] varchar(40),
  [profileImageUrl] varchar(254) NULL
)
GO

CREATE TABLE [UserBooks] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [bookId] int,
  [userId] int,
  [startDate] datetime,
  [endDate] datetime,
  [ratingId] int NULL,
  [statusId] int,
  [review] nvarchar(255) NULL,
  [reviewCreatedAt] datetime,
  [reviewEditedAt] datetime
)
GO

CREATE TABLE [BookStatuses] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [bookStatus] nvarchar(255)
)
GO

CREATE TABLE [UserShelf] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [name] nvarchar(255)
)
GO

CREATE TABLE [BookShelf] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userShelfId] int,
  [bookId] int
)
GO

CREATE TABLE [UserPosts] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postTypeId] int,
  [bookId] int NULL,
  [title] nvarchar(255) NULL,
  [cloudinaryUrl] nvarchar(255) NULL,
  [caption] nvarchar(255) NULL,
  [source] nvarchar(255) NULL,
  [songUrl] nvarchar(255) NULL,
  [songUrlSummary] nvarchar(255) NULL,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [PostTypes] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [postTypeName] nvarchar(255)
)
GO

CREATE TABLE [PostComments] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postId] int,
  [comment] nvarchar(255),
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [PostLikes] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postId] int,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [PostFavorites] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postId] int,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [PostShares] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [senderUserId] int,
  [receiverUserId] int,
  [postId] int,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [PostReblogs] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [originalPostId] int,
  [reblogPostId] int,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [Messages] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [friendId] int,
  [message] nvarchar(255),
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [Tags] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255)
)
GO

CREATE TABLE [PostTags] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [postId] int,
  [tagId] int
)
GO

CREATE TABLE [Notifications] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255)
)
GO

CREATE TABLE [UserNotifications] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [notificationId] int
)
GO

-------------------------------------------------------------------------------

ALTER TABLE [UserRoles] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Roles] ADD FOREIGN KEY ([id]) REFERENCES [UserRoles] ([roleId])
GO

ALTER TABLE [Permissions] ADD FOREIGN KEY ([roleId]) REFERENCES [Roles] ([id])
GO

ALTER TABLE [UserProfiles] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [UserBooks] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Books] ADD FOREIGN KEY ([id]) REFERENCES [UserBooks] ([bookId])
GO

ALTER TABLE [Users] ADD FOREIGN KEY ([id]) REFERENCES [Authors] ([userId])
GO

ALTER TABLE [UserFollowers] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [UserFollowers] ADD FOREIGN KEY ([followerId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [UserPosts] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [PostTypes] ADD FOREIGN KEY ([id]) REFERENCES [UserPosts] ([postTypeId])
GO

ALTER TABLE [PostTags] ADD FOREIGN KEY ([postId]) REFERENCES [UserPosts] ([id])
GO

ALTER TABLE [PostTags] ADD FOREIGN KEY ([tagId]) REFERENCES [Tags] ([id])
GO

ALTER TABLE [PostComments] ADD FOREIGN KEY ([postId]) REFERENCES [UserPosts] ([id])
GO

ALTER TABLE [PostComments] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Messages] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Messages] ADD FOREIGN KEY ([friendId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [Ratings] ADD FOREIGN KEY ([id]) REFERENCES [UserBooks] ([ratingId])
GO

ALTER TABLE [BookStatuses] ADD FOREIGN KEY ([id]) REFERENCES [UserBooks] ([statusId])
GO

ALTER TABLE [Books] ADD FOREIGN KEY ([id]) REFERENCES [UserPosts] ([bookId])
GO

ALTER TABLE [UserShelf] ADD FOREIGN KEY ([id]) REFERENCES [BookShelf] ([userShelfId])
GO

ALTER TABLE [BookShelf] ADD FOREIGN KEY ([bookId]) REFERENCES [Books] ([id])
GO

ALTER TABLE [UserShelf] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [UserNotifications] ADD FOREIGN KEY ([notificationId]) REFERENCES [Notifications] ([id])
GO

ALTER TABLE [UserNotifications] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [PostFavorites] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [PostLikes] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [PostLikes] ADD FOREIGN KEY ([postId]) REFERENCES [UserPosts] ([id])
GO

ALTER TABLE [PostFavorites] ADD FOREIGN KEY ([postId]) REFERENCES [UserPosts] ([id])
GO

ALTER TABLE [Series] ADD FOREIGN KEY ([bookId]) REFERENCES [Books] ([id])
GO

ALTER TABLE [Series] ADD FOREIGN KEY ([collectionId]) REFERENCES [SeriesCollection] ([id])
GO

ALTER TABLE [Books] ADD FOREIGN KEY ([firstAuthorId]) REFERENCES [Authors] ([id])
GO

ALTER TABLE [Books] ADD FOREIGN KEY ([secondAuthorId]) REFERENCES [Authors] ([id])
GO

ALTER TABLE [Books] ADD FOREIGN KEY ([thirdAuthorId]) REFERENCES [Authors] ([id])
GO

ALTER TABLE [SeriesPosition] ADD FOREIGN KEY ([id]) REFERENCES [Series] ([positionId])
GO

ALTER TABLE [UserPronouns] ADD FOREIGN KEY ([id]) REFERENCES [UserProfiles] ([pronounId])
GO

ALTER TABLE [PostShares] ADD FOREIGN KEY ([senderUserId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [PostShares] ADD FOREIGN KEY ([receiverUserId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [PostShares] ADD FOREIGN KEY ([postId]) REFERENCES [UserPosts] ([id])
GO

ALTER TABLE [PostReblogs] ADD FOREIGN KEY ([userId]) REFERENCES [Users] ([id])
GO

ALTER TABLE [PostReblogs] ADD FOREIGN KEY ([originalPostId]) REFERENCES [UserPosts] ([id])
GO

ALTER TABLE [PostReblogs] ADD FOREIGN KEY ([reblogPostId]) REFERENCES [UserPosts] ([id])
GO

------------------------------------------------------------------------------------------------

--table data for UserPronouns
SET IDENTITY_INSERT [UserPronouns] ON
INSERT INTO [UserPronouns]
	([id], [pronouns])
VALUES
	(1, 'he/him/his'),
	(2, 'she/her/hers'),
	(3, 'they/them')
SET IDENTITY_INSERT [UserPronouns] OFF

--table data for Roles
SET IDENTITY_INSERT [Roles] ON
INSERT INTO [Roles]
	([id], [roleName], [createdAt], [updatedAt])
VALUES
	(1, 'user', '2022-11-25 11:45:00', '2022-11-25 11:45:00'),
	(2, 'author', '2022-11-25 11:45:00', '2022-11-25 11:45:00'),
	(3, 'librarian', '2022-11-25 11:45:00', '2022-11-25 11:45:00')
SET IDENTITY_INSERT [Roles] OFF

--table data for Ratings
SET IDENTITY_INSERT [Ratings] ON
INSERT INTO [Ratings]
	([id], [displayValue])
VALUES
	(1, 0.5),
	(2, 1),
	(3, 1.5),
	(4, 2),
	(5, 2.5),
	(6, 3),
	(7, 3.5),
	(8, 4),
	(9, 4.5),
	(10, 5)
SET IDENTITY_INSERT [Ratings] OFF

--table data for BookStatuses
SET IDENTITY_INSERT [BookStatuses] ON
INSERT INTO [BookStatuses]
	([id], [bookStatus])
VALUES
	(1, 'To be read'),
	(2, 'Currently reading'),
	(3, 'Read'),
	(4, 'Did not finish')
SET IDENTITY_INSERT [BookStatuses] OFF

--table data for PostTypes
SET IDENTITY_INSERT [PostTypes] ON
INSERT INTO [PostTypes]
	([id], [postTypeName])
VALUES
	(1, 'Text'),
	(2, 'Photo'),
	(3, 'Quote'),
	(4, 'Link'),
	(5, 'Chat'),
	(6, 'Audio'),
	(7, 'Video')
SET IDENTITY_INSERT [PostTypes] OFF

--table data for SeriesPosition
SET IDENTITY_INSERT [SeriesPosition] ON
INSERT INTO [SeriesPosition]
	([id], [position])
VALUES
	(1, 1),
	(2, 2),
	(3, 3),
	(4, 4),
	(5, 5),
	(6, 6),
	(7, 7),
	(8, 8),
	(9, 9)
	(10, 10)
SET IDENTITY_INSERT [SeriesPosition] OFF