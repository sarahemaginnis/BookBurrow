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

CREATE TABLE [User] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [firebaseUID] nvarchar(255),
  [email] nvarchar(255),
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [UserProfile] (
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

  CONSTRAINT FK_UserProfile_User FOREIGN KEY (userId) REFERENCES [User](id)
)
GO

CREATE TABLE [UserPronoun] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [pronouns] varchar(40)
)
GO

CREATE TABLE [UserRole] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [roleId] int,
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_UserRole_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_UserRole_Role FOREIGN KEY (roleId) REFERENCES [Role](id)
)
GO

CREATE TABLE [Role] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [roleName] varchar(254),
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [Permission] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [permissionName] varchar(254),
  [roleId] int,
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_Permission_Role FOREIGN KEY (roleId) REFERENCES [Role](id)
)
GO

CREATE TABLE [UserFollower] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [followerId] int,
  [createdAt] datetime

  CONSTRAINT FK_UserFollower_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_UserFollower_User FOREIGN KEY (followerId) REFERENCES [User](id)
)
GO

CREATE TABLE [Book] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [title] nvarchar(255),
  [isbn] nvarchar(255) NULL,
  [description] nvarchar(255) NULL,
  [coverImageUrl] varchar(254) NULL,
  [datePublished] datetime,
  [createdAt] datetime,
  [updatedAt] datetime
)
GO

CREATE TABLE [BookAuthor] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [bookId] int,
  [authorId] int

  CONSTRAINT FK_BookAuthor_Book FOREIGN KEY (bookId) REFERENCES Book(id)
  CONSTRAINT FK_BookAuthor_Author FOREIGN KEY (authorId) REFERENCES Author(id)
)
GO

CREATE TABLE [SeriesBook] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [bookId] int,
  [seriesId] int,
  [position] int

  CONSTRAINT FK_SeriesBook_Book FOREIGN KEY (bookId) REFERENCES Book(id)
  CONSTRAINT FK_SeriesBook_Series FOREIGN KEY (seriesId) REFERENCES Series(id)
)
GO

CREATE TABLE [Series] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255)
)
GO

CREATE TABLE [Rating] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [displayValue] decimal
)
GO

CREATE TABLE [Author] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int NULL,
  [firstName] varchar(40) NULL,
  [middleName] varchar(40) NULL,
  [lastName] varchar(40),
  [profileImageUrl] varchar(254) NULL

  CONSTRAINT FK_Author_User FOREIGN KEY (userId) REFERENCES [User](id)
)
GO

CREATE TABLE [UserBook] (
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

  CONSTRAINT FK_UserBook_Book FOREIGN KEY (bookId) REFERENCES Book(id)
  CONSTRAINT FK_UserBook_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_UserBook_Rating FOREIGN KEY (ratingId) REFERENCES Rating(id)
  CONSTRAINT FK_UserBook_Status FOREIGN KEY (statusId) REFERENCES BookStatus(id)
)
GO

CREATE TABLE [BookStatus] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [bookStatus] nvarchar(255)
)
GO

CREATE TABLE [UserShelf] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [name] nvarchar(255)

  CONSTRAINT FK_UserShelf_User FOREIGN KEY (userId) REFERENCES [User](id)
)
GO

CREATE TABLE [BookShelf] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userShelfId] int,
  [bookId] int

  CONSTRAINT FK_BookShelf_UserShelf FOREIGN KEY (userShelfId) REFERENCES UserShelf(id)
  CONSTRAINT FK_BookShelf_Book FOREIGN KEY (bookId) REFERENCES Book(id)
)
GO

CREATE TABLE [UserPost] (
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

  CONSTRAINT FK_UserPost_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_UserPost_PostType FOREIGN KEY (postTypeId) REFERENCES PostType(id)
  CONSTRAINT FK_UserPost_Book FOREIGN KEY (bookId) REFERENCES Book(id)
)
GO

CREATE TABLE [PostType] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [postTypeName] nvarchar(255)
)
GO

CREATE TABLE [PostComment] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postId] int,
  [comment] nvarchar(255),
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_PostComment_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_PostComment_UserPost FOREIGN KEY (postId) REFERENCES UserPost(id)
)
GO

CREATE TABLE [PostLike] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postId] int,
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_PostLike_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_PostLike_UsesrPost FOREIGN KEY (postId) REFERENCES UserPost(id)
)
GO

CREATE TABLE [PostFavorite] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postId] int,
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_PostFavorite_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_PostFavorite_UserPost FOREIGN KEY (postId) REFERENCES UserPost(id)
)
GO

CREATE TABLE [PostShare] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [senderUserId] int,
  [receiverUserId] int,
  [postId] int,
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_PostShare_User FOREIGN KEY (senderUserId) REFERENCES [User](id)
  CONSTRAINT FK_PostShare_User FOREIGN KEY (receiverUserId) REFERENCES [User](id)
  CONSTRAINT FK_PostShare_UserPost FOREIGN KEY (postId) REFERENCES UserPost(id)
)
GO

CREATE TABLE [PostReblog] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [originalPostId] int,
  [reblogPostId] int,
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_PostReblog_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_PostReblog_UserPost FOREIGN KEY (originalPostId) REFERENCES UserPost(id)
  CONSTRAINT FK_PostReblog_UserPost FOREIGN KEY (reblogPostId) REFERENCES UserPost(id)
)
GO

CREATE TABLE [Message] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [friendId] int,
  [message] nvarchar(255),
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_Message_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_Message_User FOREIGN KEY (friendId) REFERENCES [User](id)
)
GO

CREATE TABLE [Tag] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255)
)
GO

CREATE TABLE [PostTag] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [postId] int,
  [tagId] int

  CONSTRAINT FK_PostTag_UserPost FOREIGN KEY (postId) REFERENCES UserPost(id)
  CONSTRAINT FK_PostTag_Tag FOREIGN KEY (tagId) REFERENCES Tag(id)
)
GO

CREATE TABLE [Notification] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255)
)
GO

CREATE TABLE [UserNotification] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [notificationId] int

  CONSTRAINT FK_UserNotification_User FOREIGN KEY (userId) REFERENCES [User](id)
  CONSTRAINT FK_UserNotification_Notification FOREIGN KEY (notificationId) REFERENCES [Notification](id)
)
GO

------------------------------------------------------------------------------------------------

--table data for UserPronoun
SET IDENTITY_INSERT [UserPronoun] ON
INSERT INTO [UserPronoun]
	([id], [pronouns])
VALUES
	(1, 'he/him/his'),
	(2, 'she/her/hers'),
	(3, 'they/them')
SET IDENTITY_INSERT [UserPronoun] OFF

--table data for Role
SET IDENTITY_INSERT [Role] ON
INSERT INTO [Role]
	([id], [roleName], [createdAt], [updatedAt])
VALUES
	(1, 'user', '2022-11-25 11:45:00', '2022-11-25 11:45:00'),
	(2, 'author', '2022-11-25 11:45:00', '2022-11-25 11:45:00'),
	(3, 'librarian', '2022-11-25 11:45:00', '2022-11-25 11:45:00')
SET IDENTITY_INSERT [Role] OFF

--table data for Rating
SET IDENTITY_INSERT [Rating] ON
INSERT INTO [Rating]
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
SET IDENTITY_INSERT [Rating] OFF

--table data for BookStatus
SET IDENTITY_INSERT [BookStatus] ON
INSERT INTO [BookStatus]
	([id], [bookStatus])
VALUES
	(1, 'To be read'),
	(2, 'Currently reading'),
	(3, 'Read'),
	(4, 'Did not finish')
SET IDENTITY_INSERT [BookStatus] OFF

--table data for PostType
SET IDENTITY_INSERT [PostType] ON
INSERT INTO [PostType]
	([id], [postTypeName])
VALUES
	(1, 'Text'),
	(2, 'Photo'),
	(3, 'Quote'),
	(4, 'Link'),
	(5, 'Chat'),
	(6, 'Audio'),
	(7, 'Video')
SET IDENTITY_INSERT [PostType] OFF

--table data for Book
SET IDENTITY_INSERT [Book] ON
INSERT INTO [Book]
	([id], [title], [isbn], [description], [coverImageUrl], [datePublished], [createdAt], [updatedAt])
VALUES
	(1, 'Legends & Lattes', '1250886082', 'The much-beloved BookTok sensation, Travis Baldree''s novel of high fantasy and low stakes. Come take a load off at Viv''s cafe, the first & only coffee shop in Thune. Grand opening! Worn out after decades of packing steel and raising hell, Viv, the orc barbarian, cashes out of the warrior’s life with one final score. A forgotten legend, a fabled artifact, and an unreasonable amount of hope lead her to the streets of Thune, where she plans to open the first coffee shop the city has ever seen. However, her dreams of a fresh start filling mugs instead of swinging swords are hardly a sure bet. Old frenemies and Thune’s shady underbelly may just upset her plans. To finally build something that will last, Viv will need some new partners, and a different kind of resolve. A hot cup of fantasy, slice-of-life with a dollop of romantic froth.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669473101/bookBurrow/legends_lattes_l8hidl.jpg', '2022-11-08 00:00:00', '2022-11-26 08:24:00', '2022-11-26 08:24:00'),
	(2, 'The House in the Cerulean Sea', '1250217318', 'A magical island. A dangerous task. A burning secret. Linus Baker leads a quiet, solitary life. At forty, he lives in a tiny house with a devious cat and his old records. As a Case Worker at the Department in Charge Of Magical Youth, he spends his days overseeing the well-being of children in government-sanctioned orphanages. When Linus is unexpectedly summoned by Extremely Upper Management he''s given a curious and highly classified assignment: travel to Marsyas Island Orphanage, where six dangerous children reside: a gnome, a sprite, a wyvern, an unidentifiable green blob, a were-Pomeranian, and the Antichrist. Linus must set aside his fears and determine whether or not they’re likely to bring about the end of days. But the children aren’t the only secret the island keeps. Their caretaker is the charming and enigmatic Arthur Parnassus, who will do anything to keep his wards safe. As Arthur and Linus grow closer, long-held secrets are exposed, and Linus must make a choice: destroy a home or watch the world burn. An enchanting story, masterfully told, The House in the Cerulean Sea is about the profound experience of discovering an unlikely family in an unexpected place—and realizing that family is yours.', 'https://covers.openlibrary.org/b/isbn/1250217318-L.jpg', '2020-03-17 00:00:00', '2022-11-26 08:40:00'),
	(3, 'The Very Secret Society of Irregular Witches', '059343935X', 'A warm and uplifting novel about an isolated witch whose opportunity to embrace a quirky new family--and a new love--changes the course of her life. As one of the few witches in Britain, Mika Moon knows she has to hide her magic, keep her head down, and stay away from other witches so their powers don''t mingle and draw attention. And as an orphan who lost her parents at a young age and was raised by strangers, she''s used to being alone and she follows the rules...with one exception: an online account, where she posts videos pretending to be a witch. She thinks no one will take it seriously. But someone does. An unexpected message arrives, begging her to travel to the remote and mysterious Nowhere House to teach three young witches how to control their magic. It breaks all of the rules, but Mika goes anyway, and is immediately tangled up in the lives and secrets of not only her three charges, but also an absent archaeologist, a retired actor, two long-suffering caretakers, and...Jamie. The handsome and prickly librarian of Nowhere House would do anything to protect the children, and as far as he''s concerned, a stranger like Mika is a threat. An irritatingly appealing threat. As Mika begins to find her place at Nowhere House, the thought of belonging somewhere begins to feel like a real possibility. But magic isn''t the only danger in the world, and when a threat comes knocking at their door, Mika will need to decide whether to risk everything to protect a found family she didn''t know she was looking for....', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669474031/bookBurrow/very_secret_society_irregular_witches_i8ey3c.jpg', '2022-08-23 00:00:00', '2022-11-26 08:45:00', '2022-11-26 08:45:00'),
	(4, 'A Psalm for the Wild-Built', '1250236215', 'Centuries before, robots of Panga gained self-awareness, laid down their tools, wandered, en masse into the wilderness, never to be seen again. They faded into myth and urban legend. Now the life of the tea monk who tells this story is upended by the arrival of a robot, there to honor the old promise of checking in. The robot cannot go back until the question of "what do people need?" is answered. But the answer to that question depends on who you ask, and how. They will need to ask it a lot. Chambers'' series asks: in a world where people have what they want, does having more matter?', 'https://covers.openlibrary.org/b/isbn/1250236215-L.jpg', '2021-07-13 00:00:00', '2022-11-26 08:50:00', '2022-11-26 08:50:00'),
	(5, 'The Girl Who Drank the Moon', '1616205679', 'Every year, the people of the Protectorate leave a baby as an offering to the witch who lives in the forest. They hope this sacrifice will keep her from terrorizing their town. But the witch in the forest, Xan, is kind and gentle. She shares her home with a wise Swamp Monster named Glerk and a Perfectly Tiny Dragon, Fyrian. Xan rescues the abandoned children and deliver them to welcoming families on the other side of the forest, nourishing the babies with starlight on the journey. One year, Xan accidentally feeds a baby moonlight instead of starlight, filling the ordinary child with extraordinary magic. Xan decides she must raise this enmagicked girl, whom she calls Luna, as her own. To keep young Luna safe from her own unwieldy power, Xan locks her magic deep inside her. When Luna approaches her thirteenth birthday, her magic begins to emerge on schedule--but Xan is far away. Meanwhile, a young man from the Protectorate is determined to free his people by killing the witch. Soon, it is up to Luna to protect those who have protected her--even if it means the end of the loving, safe world she’s always known.', 'https://covers.openlibrary.org/b/isbn/1616205679-L.jpg', '2016-08-09 00:00:00', '2022-11-26 08:54:00', '2022-11-26 08:54:00'),
	(6, 'Mooncakes', '154930304X', 'A story of love and demons, family and witchcraft. Nova Huang knows more about magic than your average teen witch. She works at her grandmothers'' bookshop, where she helps them loan out spell books and investigate any supernatural occurrences in their New England town. One fateful night, she follows reports of a white wolf into the woods, and she comes across the unexpected: her childhood crush, Tam Lang, battling a horse demon in the woods. As a werewolf, Tam has been wandering from place to place for years, unable to call any town home. Pursued by dark forces eager to claim the magic of wolves and out of options, Tam turns to Nova for help. Their latent feelings are rekindled against the backdrop of witchcraft, untested magic, occult rituals, and family ties both new and old in this enchanting tale of self-discovery.', 'https://covers.openlibrary.org/b/isbn/154930304X-L.jpg', '2019-10-22 00:00:00', '2022-11-26 08:58:00', '2022-11-26 08:58:00'),
	(7, 'Dealing with Dragons', '9780152045661', 'Cimorene is everything a princess is not supposed to be: headstrong, tomboyish, smart - and bored. So bored that she runs away to live with a dragon - and finds the family and excitement she''s been looking for.', 'https://covers.openlibrary.org/b/isbn/9780152045661-L.jpg', '2002-11-01 00:00:00', '2022-11-26 11:01:00', '2022-11-26 11:01:00'),
	(8, 'The Long Way to a Small, Angry Planet', '1473619815', 'Follow a motley crew on an exciting journey through space-and one adventurous young explorer who discovers the meaning of family in the far reaches of the universe-in this light-hearted debut space opera from a rising sci-fi star. Rosemary Harper doesn’t expect much when she joins the crew of the aging Wayfarer. While the patched-up ship has seen better days, it offers her a bed, a chance to explore the far-off corners of the galaxy, and most importantly, some distance from her past. An introspective young woman who learned early to keep to herself, she’s never met anyone remotely like the ship’s diverse crew, including Sissix, the exotic reptilian pilot, chatty engineers Kizzy and Jenks who keep the ship running, and Ashby, their noble captain. Life aboard the Wayfarer is chaotic and crazy—exactly what Rosemary wants. It’s also about to get extremely dangerous when the crew is offered the job of a lifetime. Tunneling wormholes through space to a distant planet is definitely lucrative and will keep them comfortable for years. But risking her life wasn’t part of the plan. In the far reaches of deep space, the tiny Wayfarer crew will confront a host of unexpected mishaps and thrilling adventures that force them to depend on each other. To survive, Rosemary’s got to learn how to rely on this assortment of oddballs—an experience that teaches her about love and trust, and that having a family isn’t necessarily the worst thing in the universe.', 'https://covers.openlibrary.org/b/isbn/1473619815-L.jpg', '2014-07-29 00:00:00', '2022-11-26 11:04:00', '2022-11-26 11:04:00'),
	(9, 'The Atlas Six', '1250854547', 'The Alexandrian Society is a secret society of magical academicians, the best in the world. Their members are caretakers of lost knowledge from the greatest civilizations of antiquity. And those who earn a place among their number will secure a life of wealth, power, and prestige beyond their wildest dreams. Each decade, the world’s six most uniquely talented magicians are selected for initiation – and here are the chosen few... - Libby Rhodes and Nicolás Ferrer de Varona: inseparable enemies, cosmologists who can control matter with their minds. - Reina Mori: a naturalist who can speak the language of life itself. - Parisa Kamali: a mind reader whose powers of seduction are unmatched. - Tristan Caine: the son of a crime kingpin who can see the secrets of the universe. - Callum Nova: an insanely rich pretty boy who could bring about the end of the world. He need only ask. When the candidates are recruited by the mysterious Atlas Blakely, they are told they must spend one year together to qualify for initiation. During this time, they will be permitted access to the Society’s archives and judged on their contributions to arcane areas of knowledge. Five, they are told, will be initiated. One will be eliminated. If they can prove themselves to be the best, they will survive. Most of them.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669482828/bookBurrow/atlas_six_hldfqx.jpg', '2020-01-31 00:00:00', '2022-11-26 11:12:00', '2022-11-26 11:12:00'),
	(10, 'Alone With You in the Ether', '1250888166', 'CHICAGO, SOMETIME—Two people meet in the Art Institute by chance. Prior to their encounter, he is a doctoral student who manages his destructive thoughts with compulsive calculations about time travel; she is a bipolar counterfeit artist, undergoing court-ordered psychotherapy. By the end of the story, these things will still be true. But this is not a story about endings. For Regan, people are predictable and tedious, including and perhaps especially herself. She copes with the dreariness of existence by living impulsively, imagining a new, alternate timeline being created in the wake of every rash decision. To Aldo, the world feels disturbingly chaotic. He gets through his days by erecting a wall of routine: a backbeat of rules and formulas that keep him going. Without them, the entire framework of his existence would collapse. For Regan and Aldo, life has been a matter of resigning themselves to the blueprints of inevitability—until the two meet. Could six conversations with a stranger be the variable that shakes up the entire simulation? From Olivie Blake, the New York Times bestselling author of The Atlas Six, comes an intimate and contemporary study of time, space, and the nature of love. Alone with You in the Ether explores what it means to be unwell, and how to face the fractures of yourself and still love as if you''re not broken.', 'https://covers.openlibrary.org/b/isbn/1250888166-L.jpg', '2020-06-20 00:00:00', '2022-11-26 11:17:00', '2022-11-26 11:17:00'),
	(11, 'The Atlas Paradox', '1250855098', '“DESTINY IS A CHOICE” The Atlas Paradox is the long-awaited sequel to dark academic sensation The Atlas Six—guaranteed to have even more yearning, backstabbing, betrayal, and chaos. Six magicians. Two rivalries. One researcher. And a man who can walk through dreams. All must pick a side: do they wish to preserve the world—or destroy it? In this electric sequel to the viral sensation, The Atlas Six, the society of Alexandrians is revealed for what it is: a secret society with raw, world-changing power, headed by a man whose plans to change life as we know it are already under way. But the cost of knowledge is steep, and as the price of power demands each character choose a side, which alliances will hold and which will see their enmity deepen?”', 'https://covers.openlibrary.org/b/isbn/1250855098-L.jpg', '2022-10-25 00:00:00', '2022-11-26 11:19:00', '2022-11-26 11:19:00'),
	(12, 'One for My Enemy', '1250892430', 'In New York City where we lay our scene, two rival witch families fight to maintain control of their respective criminal ventures. On one side of the conflict are the Antonova sisters, each one beautiful, cunning, and ruthless, and their mother, the elusive supplier of premium intoxicants known only as Baba Yaga. On the other side, the influential Fedorov brothers serve their father, the crime boss known as Koschei the Deathless, whose community extortion ventures dominate the shadows of magical Manhattan. After twelve years of tenuous coexistence, a change in one family’s interests causes a rift in the existing stalemate. When bad blood brings both families to the precipice of disaster, fate intervenes with a chance encounter, and in the aftershocks of a resurrected conflict, everyone must choose a side. As each of the siblings struggles to stake their claim, fraying loyalties threaten to rot each side from the inside out. If, that is, the enmity between empires doesn’t destroy them first.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669483382/bookBurrow/one_for_my_enemy_jgdoak.jpg', '2019-01-30 00:00:00', '2022-11-26 11:21:00', '2022-11-26 11:21:00'),
	(13, 'Clean', NULL, 'Malfoy''s handsome face was contoured into a condescending smirk. "No faith in that giant brain of yours, Granger?" She looked up at him defiantly. "Maybe I don''t have faith in you!" she said, raising her voice. Malfoy only looked at her. "You''ll find I''m very surprising." Dramione AU, Year 6 with a slow burn and a killer twist. Book I of "This World or Any Other" series. COMPLETE.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669483548/bookBurrow/clean_olivieblake_ekbi6j.jpg', '2016-04-06 00:00:00', '2022-11-26 11:26:00', '2022-11-26 11:26:00'),
	(14, 'Trial of the Valkyrie', NULL, '**Inspired by OUTLANDER - Daily releases starting 4/09 -- 480+ pages complete** Following the events of ACOSF, the Valkyrie must answer for their victory in the Blood Rite and face trial before the Illyrian Tribunal in Windhaven. Corporal punishment is certain, but there is one way around it for Gwyneth Berdara: Marriage. “As Gwyneth Berdara’s husband, I invoke the right to accept whatever punishment is determined on her behalf.” Slowly, the three Magistrates’ heads swiveled to Gwyn. The priestess swallowed hard. “Miss Berdara,” Crispin said, “You can confirm that you are the wife of Spymaster Azriel?” She did not hesitate. “Yes.” Celio’s brows furrowed, while Crispin exhaled heavily. “Thank you, for confirming, Miss Berdara,” Fabius said, his voice slick with mock amiability. “Please note that we reserve the right to call your union into question if we find justification.” He shifted his attention to Azriel. “Sustained, Spymaster Azriel.”', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484273/bookBurrow/bookBurrow_default_book_cover_picture_edqycj.png', '2022-04-08 00:00:00', '2022-11-26 00:00:00', '2022-11-26 00:00:00'),
	(15, 'Measure of a Man', NULL, 'To truly know someone is to differentiate between who they once were, who they are now, and who they''re capable of being. Hermione realises the duality of one man as she rectifies what she knows of the past and begins to understand the pieces of who Draco Malfoy is now: a father, a son, and a man.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484273/bookBurrow/bookBurrow_default_book_cover_picture_edqycj.png', '2020-09-18 00:00:00', '2022-11-26 11:39:00', '2022-11-26 11:39:00'),
	(16, 'Breath Mints / Battle Scars', NULL, 'For a moment, she''s almost giddy. Because Draco Malfoy''s been ruined by this war and he''s as out of place as she is and — yes, he has scars too. He''s got an even bigger one. She wonders whether one day they''ll compare sizes. Fanfiction for Harry Potter by J.K. Rowling.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484973/bookBurrow/breath_mints_battle_scars_jkzcnz.jpg', '2018-07-21 00:00:00', '2022-11-26 11:48:00', '2022-11-26 11:48:00'),
	(17, 'Love and Other Historical Accidents', NULL, 'Hermione Granger and Draco Malfoy never intended to blow up their life''s work, but that''s rather what they''ve gone and done. Now they''re trapped 200 years in the past, with a broken Time Turner, a missing snuff box, a handful of overly-eligible daughters, and a House-elf in a cable knit cardigan. It will require the combined power of their keen intellects to get them home, if they''d stop arguing long enough to use them. As it turns out, history is just one damned accident after another. For fans of Harry Potter, Jane Austen, and Connie Willis, a historical romantic comedy all about time, and getting the hell out of it.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669485194/bookBurrow/love_other_historical_accidents_y2wndb.jpg', '2019-11-20 00:00:00', '2022-11-26 11:51:00', '2022-11-26 11:51:00'),
	(18, 'Snowy-Peaked Promises', NULL, 'Hermione''s quiet Christmas plans with Harry are upended when, halfway across the world, they come across a trio of former Slytherins staying in the next cabin over. But Hermione isn''t entirely certain whether quiet is what she wants anymore. Dramione.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484273/bookBurrow/bookBurrow_default_book_cover_picture_edqycj.png', '2021-12-21 00:00:00', '2022-11-26 11:55:00', '2022-11-26 11:55:00'),
	(19, '')
SET IDENTITY_INSERT [Book] OFF