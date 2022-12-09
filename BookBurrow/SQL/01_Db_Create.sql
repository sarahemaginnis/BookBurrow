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

  CONSTRAINT FK_UserRole_User FOREIGN KEY (userId) REFERENCES [User](id),
)
GO

CREATE TABLE [Permission] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [permissionName] varchar(254),
  [roleId] int,
  [createdAt] datetime,
  [updatedAt] datetime
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

CREATE TABLE [UserFollower] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [followerId] int,
  [createdAt] datetime

  CONSTRAINT FK_UserFollower_User FOREIGN KEY (userId) REFERENCES [User](id),
  FOREIGN KEY (followerId) REFERENCES [User](id)
)
GO

CREATE TABLE [Book] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [title] nvarchar(255),
  [isbn] nvarchar(255) NULL,
  [description] nvarchar(max) NULL,
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

  CONSTRAINT FK_BookAuthor_Book FOREIGN KEY (bookId) REFERENCES Book(id),
  CONSTRAINT FK_BookAuthor_Author FOREIGN KEY (authorId) REFERENCES Author(id)
)
GO

CREATE TABLE [Series] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255)
)
GO

CREATE TABLE [SeriesBook] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [bookId] int,
  [seriesId] int,
  [position] int

  CONSTRAINT FK_SeriesBook_Book FOREIGN KEY (bookId) REFERENCES Book(id),
  CONSTRAINT FK_SeriesBook_Series FOREIGN KEY (seriesId) REFERENCES Series(id)
)
GO

CREATE TABLE [Rating] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [displayValue] decimal (2, 1)
)
GO

CREATE TABLE [UserBook] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [bookId] int,
  [userId] int,
  [startDate] datetime NULL,
  [endDate] datetime NULL,
  [ratingId] int NULL,
  [statusId] int,
  [review] nvarchar(255) NULL,
  [reviewCreatedAt] datetime,
  [reviewEditedAt] datetime

  CONSTRAINT FK_UserBook_Book FOREIGN KEY (bookId) REFERENCES Book(id),
  CONSTRAINT FK_UserBook_User FOREIGN KEY (userId) REFERENCES [User](id),
  CONSTRAINT FK_UserBook_Rating FOREIGN KEY (ratingId) REFERENCES Rating(id),
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

  CONSTRAINT FK_BookShelf_UserShelf FOREIGN KEY (userShelfId) REFERENCES UserShelf(id),
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
  [caption] nvarchar(max) NULL,
  [source] nvarchar(255) NULL,
  [songUrl] nvarchar(255) NULL,
  [songUrlSummary] nvarchar(255) NULL,
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_UserPost_User FOREIGN KEY (userId) REFERENCES [User](id),
  CONSTRAINT FK_UserPost_Book FOREIGN KEY (bookId) REFERENCES Book(id)
)
GO

CREATE TABLE [PostComment] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postId] int,
  [comment] nvarchar(255),
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_PostComment_User FOREIGN KEY (userId) REFERENCES [User](id),
  CONSTRAINT FK_PostComment_UserPost FOREIGN KEY (postId) REFERENCES UserPost(id)
)
GO

CREATE TABLE [PostLike] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postId] int,
  [createdAt] datetime

  CONSTRAINT FK_PostLike_User FOREIGN KEY (userId) REFERENCES [User](id),
  CONSTRAINT FK_PostLike_UsesrPost FOREIGN KEY (postId) REFERENCES UserPost(id)
)
GO

CREATE TABLE [PostFavorite] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [postId] int,
  [createdAt] datetime

  CONSTRAINT FK_PostFavorite_User FOREIGN KEY (userId) REFERENCES [User](id),
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

  CONSTRAINT FK_PostShare_User FOREIGN KEY (senderUserId) REFERENCES [User](id),
  FOREIGN KEY (receiverUserId) REFERENCES [User](id),
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

  CONSTRAINT FK_PostReblog_User FOREIGN KEY (userId) REFERENCES [User](id),
  CONSTRAINT FK_PostReblog_UserPost FOREIGN KEY (originalPostId) REFERENCES UserPost(id),
  FOREIGN KEY (reblogPostId) REFERENCES UserPost(id)
)
GO

CREATE TABLE [Message] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [userId] int,
  [friendId] int,
  [message] nvarchar(255),
  [createdAt] datetime,
  [updatedAt] datetime

  CONSTRAINT FK_Message_User FOREIGN KEY (userId) REFERENCES [User](id),
  FOREIGN KEY (friendId) REFERENCES [User](id)
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

  CONSTRAINT FK_PostTag_UserPost FOREIGN KEY (postId) REFERENCES UserPost(id),
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

  CONSTRAINT FK_UserNotification_User FOREIGN KEY (userId) REFERENCES [User](id),
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

--table data for Rating
SET IDENTITY_INSERT [Rating] ON
INSERT INTO [Rating]
	([id], [displayValue])
VALUES
	(1, 0.5),
	(2, 1.0),
	(3, 1.5),
	(4, 2.0),
	(5, 2.5),
	(6, 3.0),
	(7, 3.5),
	(8, 4.0),
	(9, 4.5),
	(10, 5.0)
SET IDENTITY_INSERT [Rating] OFF

--table data for Book
SET IDENTITY_INSERT [Book] ON
INSERT INTO [Book]
	([id], [title], [isbn], [description], [coverImageUrl], [datePublished], [createdAt], [updatedAt])
VALUES
	(1, 'Legends & Lattes', '1250886082', 'The much-beloved BookTok sensation, Travis Baldree''s novel of high fantasy and low stakes. Come take a load off at Viv''s cafe, the first & only coffee shop in Thune. Grand opening! Worn out after decades of packing steel and raising hell, Viv, the orc barbarian, cashes out of the warrior''s life with one final score. A forgotten legend, a fabled artifact, and an unreasonable amount of hope lead her to the streets of Thune, where she plans to open the first coffee shop the city has ever seen. However, her dreams of a fresh start filling mugs instead of swinging swords are hardly a sure bet. Old frenemies and Thune''s shady underbelly may just upset her plans. To finally build something that will last, Viv will need some new partners, and a different kind of resolve. A hot cup of fantasy, slice-of-life with a dollop of romantic froth.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669473101/bookBurrow/legends_lattes_l8hidl.jpg', '2022-11-08 00:00:00', '2022-11-26 08:24:00', '2022-11-26 08:24:00'),
	(2, 'The House in the Cerulean Sea', '1250217318', 'A magical island. A dangerous task. A burning secret. Linus Baker leads a quiet, solitary life. At forty, he lives in a tiny house with a devious cat and his old records. As a Case Worker at the Department in Charge Of Magical Youth, he spends his days overseeing the well-being of children in government-sanctioned orphanages. When Linus is unexpectedly summoned by Extremely Upper Management he''s given a curious and highly classified assignment: travel to Marsyas Island Orphanage, where six dangerous children reside: a gnome, a sprite, a wyvern, an unidentifiable green blob, a were-Pomeranian, and the Antichrist. Linus must set aside his fears and determine whether or not they''re likely to bring about the end of days. But the children aren''t the only secret the island keeps. Their caretaker is the charming and enigmatic Arthur Parnassus, who will do anything to keep his wards safe. As Arthur and Linus grow closer, long-held secrets are exposed, and Linus must make a choice: destroy a home or watch the world burn. An enchanting story, masterfully told, The House in the Cerulean Sea is about the profound experience of discovering an unlikely family in an unexpected place—and realizing that family is yours.', 'https://covers.openlibrary.org/b/isbn/1250217318-L.jpg', '2020-03-17 00:00:00', '2022-11-26 08:40:00', '2022-11-26 08:40:00'),
	(3, 'The Very Secret Society of Irregular Witches', '059343935X', 'A warm and uplifting novel about an isolated witch whose opportunity to embrace a quirky new family--and a new love--changes the course of her life. As one of the few witches in Britain, Mika Moon knows she has to hide her magic, keep her head down, and stay away from other witches so their powers don''t mingle and draw attention. And as an orphan who lost her parents at a young age and was raised by strangers, she''s used to being alone and she follows the rules...with one exception: an online account, where she posts videos pretending to be a witch. She thinks no one will take it seriously. But someone does. An unexpected message arrives, begging her to travel to the remote and mysterious Nowhere House to teach three young witches how to control their magic. It breaks all of the rules, but Mika goes anyway, and is immediately tangled up in the lives and secrets of not only her three charges, but also an absent archaeologist, a retired actor, two long-suffering caretakers, and...Jamie. The handsome and prickly librarian of Nowhere House would do anything to protect the children, and as far as he''s concerned, a stranger like Mika is a threat. An irritatingly appealing threat. As Mika begins to find her place at Nowhere House, the thought of belonging somewhere begins to feel like a real possibility. But magic isn''t the only danger in the world, and when a threat comes knocking at their door, Mika will need to decide whether to risk everything to protect a found family she didn''t know she was looking for....', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669474031/bookBurrow/very_secret_society_irregular_witches_i8ey3c.jpg', '2022-08-23 00:00:00', '2022-11-26 08:45:00', '2022-11-26 08:45:00'),
	(4, 'A Psalm for the Wild-Built', '1250236215', 'Centuries before, robots of Panga gained self-awareness, laid down their tools, wandered, en masse into the wilderness, never to be seen again. They faded into myth and urban legend. Now the life of the tea monk who tells this story is upended by the arrival of a robot, there to honor the old promise of checking in. The robot cannot go back until the question of "what do people need?" is answered. But the answer to that question depends on who you ask, and how. They will need to ask it a lot. Chambers'' series asks: in a world where people have what they want, does having more matter?', 'https://covers.openlibrary.org/b/isbn/1250236215-L.jpg', '2021-07-13 00:00:00', '2022-11-26 08:50:00', '2022-11-26 08:50:00'),
	(5, 'The Girl Who Drank the Moon', '1616205679', 'Every year, the people of the Protectorate leave a baby as an offering to the witch who lives in the forest. They hope this sacrifice will keep her from terrorizing their town. But the witch in the forest, Xan, is kind and gentle. She shares her home with a wise Swamp Monster named Glerk and a Perfectly Tiny Dragon, Fyrian. Xan rescues the abandoned children and deliver them to welcoming families on the other side of the forest, nourishing the babies with starlight on the journey. One year, Xan accidentally feeds a baby moonlight instead of starlight, filling the ordinary child with extraordinary magic. Xan decides she must raise this enmagicked girl, whom she calls Luna, as her own. To keep young Luna safe from her own unwieldy power, Xan locks her magic deep inside her. When Luna approaches her thirteenth birthday, her magic begins to emerge on schedule--but Xan is far away. Meanwhile, a young man from the Protectorate is determined to free his people by killing the witch. Soon, it is up to Luna to protect those who have protected her--even if it means the end of the loving, safe world she''s always known.', 'https://covers.openlibrary.org/b/isbn/1616205679-L.jpg', '2016-08-09 00:00:00', '2022-11-26 08:54:00', '2022-11-26 08:54:00'),
	(6, 'Mooncakes', '154930304X', 'A story of love and demons, family and witchcraft. Nova Huang knows more about magic than your average teen witch. She works at her grandmothers'' bookshop, where she helps them loan out spell books and investigate any supernatural occurrences in their New England town. One fateful night, she follows reports of a white wolf into the woods, and she comes across the unexpected: her childhood crush, Tam Lang, battling a horse demon in the woods. As a werewolf, Tam has been wandering from place to place for years, unable to call any town home. Pursued by dark forces eager to claim the magic of wolves and out of options, Tam turns to Nova for help. Their latent feelings are rekindled against the backdrop of witchcraft, untested magic, occult rituals, and family ties both new and old in this enchanting tale of self-discovery.', 'https://covers.openlibrary.org/b/isbn/154930304X-L.jpg', '2019-10-22 00:00:00', '2022-11-26 08:58:00', '2022-11-26 08:58:00'),
	(7, 'Dealing with Dragons', '9780152045661', 'Cimorene is everything a princess is not supposed to be: headstrong, tomboyish, smart - and bored. So bored that she runs away to live with a dragon - and finds the family and excitement she''s been looking for.', 'https://covers.openlibrary.org/b/isbn/9780152045661-L.jpg', '2002-11-01 00:00:00', '2022-11-26 11:01:00', '2022-11-26 11:01:00'),
	(8, 'The Long Way to a Small, Angry Planet', '1473619815', 'Follow a motley crew on an exciting journey through space-and one adventurous young explorer who discovers the meaning of family in the far reaches of the universe-in this light-hearted debut space opera from a rising sci-fi star. Rosemary Harper doesn’t expect much when she joins the crew of the aging Wayfarer. While the patched-up ship has seen better days, it offers her a bed, a chance to explore the far-off corners of the galaxy, and most importantly, some distance from her past. An introspective young woman who learned early to keep to herself, she''s never met anyone remotely like the ship''s diverse crew, including Sissix, the exotic reptilian pilot, chatty engineers Kizzy and Jenks who keep the ship running, and Ashby, their noble captain. Life aboard the Wayfarer is chaotic and crazy—exactly what Rosemary wants. It''s also about to get extremely dangerous when the crew is offered the job of a lifetime. Tunneling wormholes through space to a distant planet is definitely lucrative and will keep them comfortable for years. But risking her life wasn''t part of the plan. In the far reaches of deep space, the tiny Wayfarer crew will confront a host of unexpected mishaps and thrilling adventures that force them to depend on each other. To survive, Rosemary''s got to learn how to rely on this assortment of oddballs—an experience that teaches her about love and trust, and that having a family isn''t necessarily the worst thing in the universe.', 'https://covers.openlibrary.org/b/isbn/1473619815-L.jpg', '2014-07-29 00:00:00', '2022-11-26 11:04:00', '2022-11-26 11:04:00'),
	(9, 'The Atlas Six', '1250854547', 'The Alexandrian Society is a secret society of magical academicians, the best in the world. Their members are caretakers of lost knowledge from the greatest civilizations of antiquity. And those who earn a place among their number will secure a life of wealth, power, and prestige beyond their wildest dreams. Each decade, the world''s six most uniquely talented magicians are selected for initiation – and here are the chosen few... - Libby Rhodes and Nicolás Ferrer de Varona: inseparable enemies, cosmologists who can control matter with their minds. - Reina Mori: a naturalist who can speak the language of life itself. - Parisa Kamali: a mind reader whose powers of seduction are unmatched. - Tristan Caine: the son of a crime kingpin who can see the secrets of the universe. - Callum Nova: an insanely rich pretty boy who could bring about the end of the world. He need only ask. When the candidates are recruited by the mysterious Atlas Blakely, they are told they must spend one year together to qualify for initiation. During this time, they will be permitted access to the Society''s archives and judged on their contributions to arcane areas of knowledge. Five, they are told, will be initiated. One will be eliminated. If they can prove themselves to be the best, they will survive. Most of them.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669482828/bookBurrow/atlas_six_hldfqx.jpg', '2020-01-31 00:00:00', '2022-11-26 11:12:00', '2022-11-26 11:12:00'),
	(10, 'Alone With You in the Ether', '1250888166', 'CHICAGO, SOMETIME—Two people meet in the Art Institute by chance. Prior to their encounter, he is a doctoral student who manages his destructive thoughts with compulsive calculations about time travel; she is a bipolar counterfeit artist, undergoing court-ordered psychotherapy. By the end of the story, these things will still be true. But this is not a story about endings. For Regan, people are predictable and tedious, including and perhaps especially herself. She copes with the dreariness of existence by living impulsively, imagining a new, alternate timeline being created in the wake of every rash decision. To Aldo, the world feels disturbingly chaotic. He gets through his days by erecting a wall of routine: a backbeat of rules and formulas that keep him going. Without them, the entire framework of his existence would collapse. For Regan and Aldo, life has been a matter of resigning themselves to the blueprints of inevitability—until the two meet. Could six conversations with a stranger be the variable that shakes up the entire simulation? From Olivie Blake, the New York Times bestselling author of The Atlas Six, comes an intimate and contemporary study of time, space, and the nature of love. Alone with You in the Ether explores what it means to be unwell, and how to face the fractures of yourself and still love as if you''re not broken.', 'https://covers.openlibrary.org/b/isbn/1250888166-L.jpg', '2020-06-20 00:00:00', '2022-11-26 11:17:00', '2022-11-26 11:17:00'),
	(11, 'The Atlas Paradox', '1250855098', '“DESTINY IS A CHOICE” The Atlas Paradox is the long-awaited sequel to dark academic sensation The Atlas Six—guaranteed to have even more yearning, backstabbing, betrayal, and chaos. Six magicians. Two rivalries. One researcher. And a man who can walk through dreams. All must pick a side: do they wish to preserve the world—or destroy it? In this electric sequel to the viral sensation, The Atlas Six, the society of Alexandrians is revealed for what it is: a secret society with raw, world-changing power, headed by a man whose plans to change life as we know it are already under way. But the cost of knowledge is steep, and as the price of power demands each character choose a side, which alliances will hold and which will see their enmity deepen?”', 'https://covers.openlibrary.org/b/isbn/1250855098-L.jpg', '2022-10-25 00:00:00', '2022-11-26 11:19:00', '2022-11-26 11:19:00'),
	(12, 'One for My Enemy', '1250892430', 'In New York City where we lay our scene, two rival witch families fight to maintain control of their respective criminal ventures. On one side of the conflict are the Antonova sisters, each one beautiful, cunning, and ruthless, and their mother, the elusive supplier of premium intoxicants known only as Baba Yaga. On the other side, the influential Fedorov brothers serve their father, the crime boss known as Koschei the Deathless, whose community extortion ventures dominate the shadows of magical Manhattan. After twelve years of tenuous coexistence, a change in one family''s interests causes a rift in the existing stalemate. When bad blood brings both families to the precipice of disaster, fate intervenes with a chance encounter, and in the aftershocks of a resurrected conflict, everyone must choose a side. As each of the siblings struggles to stake their claim, fraying loyalties threaten to rot each side from the inside out. If, that is, the enmity between empires doesn''t destroy them first.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669483382/bookBurrow/one_for_my_enemy_jgdoak.jpg', '2019-01-30 00:00:00', '2022-11-26 11:21:00', '2022-11-26 11:21:00'),
	(13, 'Clean', NULL, 'Malfoy''s handsome face was contoured into a condescending smirk. "No faith in that giant brain of yours, Granger?" She looked up at him defiantly. "Maybe I don''t have faith in you!" she said, raising her voice. Malfoy only looked at her. "You''ll find I''m very surprising." Dramione AU, Year 6 with a slow burn and a killer twist. Book I of "This World or Any Other" series. COMPLETE.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669483548/bookBurrow/clean_olivieblake_ekbi6j.jpg', '2016-04-06 00:00:00', '2022-11-26 11:26:00', '2022-11-26 11:26:00'),
	(14, 'Trial of the Valkyrie', NULL, '**Inspired by OUTLANDER - Daily releases starting 4/09 -- 480+ pages complete** Following the events of ACOSF, the Valkyrie must answer for their victory in the Blood Rite and face trial before the Illyrian Tribunal in Windhaven. Corporal punishment is certain, but there is one way around it for Gwyneth Berdara: Marriage. “As Gwyneth Berdara''s husband, I invoke the right to accept whatever punishment is determined on her behalf.” Slowly, the three Magistrates'' heads swiveled to Gwyn. The priestess swallowed hard. “Miss Berdara,” Crispin said, “You can confirm that you are the wife of Spymaster Azriel?” She did not hesitate. “Yes.” Celio''s brows furrowed, while Crispin exhaled heavily. “Thank you, for confirming, Miss Berdara,” Fabius said, his voice slick with mock amiability. “Please note that we reserve the right to call your union into question if we find justification.” He shifted his attention to Azriel. “Sustained, Spymaster Azriel.”', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484273/bookBurrow/bookBurrow_default_book_cover_picture_edqycj.png', '2022-04-08 00:00:00', '2022-11-26 00:00:00', '2022-11-26 00:00:00'),
	(15, 'Measure of a Man', NULL, 'To truly know someone is to differentiate between who they once were, who they are now, and who they''re capable of being. Hermione realises the duality of one man as she rectifies what she knows of the past and begins to understand the pieces of who Draco Malfoy is now: a father, a son, and a man.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484273/bookBurrow/bookBurrow_default_book_cover_picture_edqycj.png', '2020-09-18 00:00:00', '2022-11-26 11:39:00', '2022-11-26 11:39:00'),
	(16, 'Breath Mints / Battle Scars', NULL, 'For a moment, she''s almost giddy. Because Draco Malfoy''s been ruined by this war and he''s as out of place as she is and — yes, he has scars too. He''s got an even bigger one. She wonders whether one day they''ll compare sizes. Fanfiction for Harry Potter by J.K. Rowling.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484973/bookBurrow/breath_mints_battle_scars_jkzcnz.jpg', '2018-07-21 00:00:00', '2022-11-26 11:48:00', '2022-11-26 11:48:00'),
	(17, 'Love and Other Historical Accidents', NULL, 'Hermione Granger and Draco Malfoy never intended to blow up their life''s work, but that''s rather what they''ve gone and done. Now they''re trapped 200 years in the past, with a broken Time Turner, a missing snuff box, a handful of overly-eligible daughters, and a House-elf in a cable knit cardigan. It will require the combined power of their keen intellects to get them home, if they''d stop arguing long enough to use them. As it turns out, history is just one damned accident after another. For fans of Harry Potter, Jane Austen, and Connie Willis, a historical romantic comedy all about time, and getting the hell out of it.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669485194/bookBurrow/love_other_historical_accidents_y2wndb.jpg', '2019-11-20 00:00:00', '2022-11-26 11:51:00', '2022-11-26 11:51:00'),
	(18, 'Snowy-Peaked Promises', NULL, 'Hermione''s quiet Christmas plans with Harry are upended when, halfway across the world, they come across a trio of former Slytherins staying in the next cabin over. But Hermione isn''t entirely certain whether quiet is what she wants anymore. Dramione.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484273/bookBurrow/bookBurrow_default_book_cover_picture_edqycj.png', '2021-12-21 00:00:00', '2022-11-26 11:55:00', '2022-11-26 11:55:00'),
	(19, 'Draco Malfoy and the Mortifying Ordeal of Being in Love', NULL, 'Hermione straddles the magical and non-magical worlds as a medical researcher and Healer about to make a Big Discovery. Draco is an Auror assigned to protect her from forces unknown -- to both of their displeasure. Features hypercompetent, fiery Hermione and lazy, yet dangerous, Draco. Slow burn.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669485498/bookBurrow/draco_malfoy_mortifying_ordeal_being_in_love_cfq3w2.jpg', '2021-10-14 00:00:00', '2022-11-26 11:57:00', '2022-11-26 11:57:00'),
	(20, 'Wait and Hope', NULL, '“Harry,” Hermione began, voice very controlled, but she could feel the blade of panic slicing at her vocal cords. “Why was Draco Malfoy just screaming bloody murder about his,” and the word almost strangled her as she said it, “wife?” Harry''s green eyes blew wide. Healer Lucas pinched the bridge of her nose, painfully displeased with the recent series of events. “He was referring to you, my dear,” she said. “That was the other question you got wrong. Your name is Hermione Jean Granger-Malfoy.” Hermione had to be sedated again. Post-Hogwarts. EWE. Memory fic. Dramione. Wordcount: 95,107', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484273/bookBurrow/bookBurrow_default_book_cover_picture_edqycj.png', '2020-02-20 00:00:00', '2022-11-26 12:00:00', '2022-11-26 12:00:00'),
	(21, 'The Auction', NULL, 'Hermione felt the pounding in her ears again. She would see him for the first time since the Great Hall, gaunt and stricken at the Slytherin table with his mother clutching his arm. She hadn''t meant to look for him. Not in the corridors, not beneath the white sheets of the fallen, not on the way to the Chamber of Secrets with Ron, but she was a stupid girl. Fandom: Harry Potter. Pairing: Hermione Granger/Draco Malfoy. Words: 181,287', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669688472/bookBurrow/auction_g3cm4q.jpg', '2019-06-05 00:00:00','2022-11-26 12:04:00', '2022-11-26 12:04:00'),
	(22, 'All the Wrong Things', NULL, 'Sequel to "The Right Thing to Do" - Draco''s POV. Part 2 of the "Rights and Wrongs" series. Friday, August 27, 1999. They''re murmuring again. Trying to keep their voices low so the prisoner can''t hear. But the prisoner is fifteen feet away, and they are failing. I wish they would take me out of the room if they need to discuss. Bring me back to the small room I was in this morning. But, of course, they let me stand in this cage in the middle of them. On display. I pick a spot four feet in front of me and maintain my gaze. I don''t want to look at them and I don''t want to fall asleep. I feel a yawn. “Mr. Malfoy. Your next witness is here. Are you ready to proceed?” I almost smile. Do I have a choice?', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669688713/bookBurrow/all_the_wrong_things_wlrxjk.jpg', '2018-04-27 00:00:00', '2022-11-28 20:24:00', '2022-11-28 20:24:00'),
	(23, 'The Right Thing to Do', NULL, 'Hermione felt the pounding in her ears again. She would see him for the first time since the Great Hall, gaunt and stricken at the Slytherin table with his mother clutching his arm. She hadn''t meant to look for him. Not in the corridors, not beneath the white sheets of the fallen, not on the way to the Chamber of Secrets with Ron, but she was a stupid girl. Fandom: Harry Potter. Pairing: Hermione Granger/Draco Malfoy. Words: 181,287', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669688823/bookBurrow/right_thing_to_do_f2xl27.jpg', '2017-07-11 00:00:00', '2022-11-28 20:26:00', '2022-11-28 20:26:00'),
	(24, 'Remain Nameless', NULL, 'How did it feel? It felt like he was barely holding it together. She, of all people, should shun him. Or yell at him. Curse him. Spit at him. Take out her wand and blast him off the face of the earth. It was crushing guilt and relief and confusion all at once when he looked at Hermione Granger. The monotony of Draco''s daily routine had become both a lifeline and a noose. But this new habit of grabbing coffee with Hermione Granger is quickly becoming a reason to get out of bed and is unfortunately forcing him to re-evaluate his inconsequential existence. Hermione is living her life in fragments, separate pieces scattered about, and she can''t find a way to step back and let the full picture form. Why are morning meetings with Draco Malfoy the only thing that make sense anymore?', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669688938/bookBurrow/remain_nameless_psndc2.jpg', '2020-04-27 00:00:00', '2022-11-28 20:28:00', '2022-11-28 20:28:00'),
	(25, 'The Odds', NULL, 'What can you get out of a writer''s block and a past acquaintance you have severe prejudices against coming together? In a desperate attempt to deal with her creative struggle, Hermione Granger rents a cabin in the Alps to isolate herself from the rest of the world. But unfortunately—or fatefully—Draco Malfoy knocks at her door, sure to have booked the cabin for himself. Stuck together in a limited space for an indefinite amount of time, Hermione slowly finds out the man Malfoy grew up to be is very different from the teenager she used to know. Maybe, just maybe, written words will manage to make amends for her inability to express her feelings.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669484273/bookBurrow/bookBurrow_default_book_cover_picture_edqycj.png', '2021-10-19 00:00:00', '2022-11-28 20:29:00', '2022-11-28 20:29:00'),
	(26, 'Bring Him to His Knees', NULL, 'Draco is on the case of a murderer, but to investigate, he needs a fake relationship - and a kink club play partner. When Hermione volunteers to take the role, both do their best to maintain the lie without letting each other know the truth: neither of them is acting.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669689134/bookBurrow/bring_him_to_his_knees_cqfl8f.jpg', '2020-06-01 00:00:00', '2022-11-28 20:31:00', '2022-11-28 20:31:00'),
	(27, 'All You Want', NULL, 'Draco/Hermione. Eighth Year at Hogwarts was supposed to be Hermione’s. And it is, just not in the way she expects. Omegaverse fic.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669689230/bookBurrow/all_you_want_vuk3bw.jpg', '2018-07-03 00:00:00', '2022-11-28 20:32:00', '2022-11-28 20:32:00'),
	(28, 'Manacled', NULL, 'Harry Potter is dead. In the aftermath of the war, in order to strengthen the might of the magical world, Voldemort enacts a repopulation effort. Hermione Granger has an Order secret, lost but hidden in her mind, so she is sent as an enslaved surrogate to the High Reeve, to be bred and monitored until her mind can be cracked. Fanfiction for Harry Potter by J. K. Rowling. Words: 370,256', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669689334/bookBurrow/manacled_sjldyj.jpg', '2018-04-27 00:00:00', '2022-11-28 20:34:00', '2022-11-28 20:34:00'),
	(29, 'The Library of Alexandria', NULL, 'The Library of Alexandria is not for just any witch or wizard. Many bookworms may try but few are permitted to pass through its doors. The books residing there are ancient and powerful and, if one happens to make a mistake, the consequences can be rather—novel.', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669689428/bookBurrow/library_alexandria_ympobd.jpg', '2018-05-04 00:00:00', '2022-11-28 20:36:00', '2022-11-28 20:36:00'),
	(30, 'Hogfather', '9781473200135', 'It''s the night before Hogswatch. And it''s too quiet. Where is the big jolly fat man? Why is Death creeping down chimneys and trying to say Ho Ho Ho? The darkest night of the year is getting a lot darker… Susan the gothic governess has got to sort it out by morning, otherwise there won''t be a morning. Ever again... The 20th Discworld novel is a festive feast of darkness and Death (but with jolly robins and tinsel too). As they say: "You''d better watch out..."', 'https://covers.openlibrary.org/b/isbn/9781473200135-L.jpg', '1996-01-01 00:00:00', '2022-11-28 20:40:00', '2022-11-28 20:40:00')
SET IDENTITY_INSERT [Book] OFF

--table data for Author
SET IDENTITY_INSERT [Author] ON
INSERT INTO [Author]
	([id], [userId], [firstName], [middleName], [lastName], [profileImageUrl])
VALUES
	(1, NULL, 'Travis', NULL, 'Baldree', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669473406/bookBurrow/travis_baldree_y1mukw.jpg'),
	(2, NULL, 'T.J.', NULL, 'Klune', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669473798/bookBurrow/tj_klune_h9hz7z.jpg'),
	(3, NULL, 'Sangu', NULL, 'Mandanna', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669474153/bookBurrow/sangu_mandanna_ohw8jf.jpg'),
	(4, NULL, 'Becky', NULL, 'Chambers', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669474359/bookBurrow/becky_chambers_oujtqs.jpg'),
	(5, NULL, 'Kelly', NULL, 'Barnhill', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669474563/bookBurrow/kelly_barnhill_icnyd8.jpg'),
	(6, NULL, 'Suzanne', NULL, 'Walker', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(7, NULL, 'Wendy', NULL, 'Xu', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(8, NULL, 'Joamette', NULL, 'Gil', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(9, NULL, 'Patricia', 'C.', 'Wrede', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669482151/bookBurrow/patricia_c_wrede_iqlrlu.jpg'),
	(10, NULL, 'Olivie', NULL, 'Blake', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669482879/bookBurrow/olivie_blake_khkibe.jpg'),
	(11, NULL, NULL, NULL, 'olivieblake', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(12, NULL, NULL, NULL, 'Daevastanner', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(13, NULL, NULL, NULL, 'inadaze22', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(14, NULL, NULL, NULL, 'Onyx_and_Elm', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(15, NULL, NULL, NULL, 'PacificRimbaud', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(16, NULL, NULL, NULL, 'In_Dreams', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(17, NULL, NULL, NULL, 'isthisselfcare', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(18, NULL, NULL, NULL, 'mightbewriting', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(19, NULL, NULL, NULL, 'SenLinYu', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(20, NULL, NULL, NULL, 'Musyc', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(21, NULL, NULL, NULL, 'sarab3lla', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(22, NULL, NULL, NULL, 'va13lentina', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(23, NULL, NULL, NULL, 'HeyJude19', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(24, NULL, NULL, NULL, 'LovesBitca8', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669475589/bookBurrow/bookBurrow_default_profile_picture_rsxmg9.png'),
	(25, NULL, 'Terry', NULL, 'Pratchett', 'https://res.cloudinary.com/dxblkicjh/image/upload/v1669737432/bookBurrow/terry_pratchett_ayjnit.jpg')
SET IDENTITY_INSERT [Author] OFF

--table data for BookAuthor
SET IDENTITY_INSERT [BookAuthor] ON
INSERT INTO [BookAuthor]
	([id], [bookId], [authorId])
VALUES
	(1, 1, 1),
	(2, 2, 2),
	(3, 3, 3),
	(4, 4, 4),
	(5, 5, 5),
	(6, 6, 6),
	(7, 6, 7),
	(8, 6, 8),
	(9, 7, 9),
	(10, 8, 4),
	(11, 9, 10),
	(12, 10, 10),
	(13, 11, 10),
	(14, 12, 10),
	(15, 13, 11),
	(16, 14, 12),
	(17, 15, 13),
	(18, 16, 14),
	(19, 17, 15),
	(20, 18, 16),
	(21, 19, 17),
	(22, 20, 18),
	(23, 21, 24),
	(24, 22, 24),
	(25, 23, 24),
	(26, 24, 23),
	(27, 25, 21),
	(28, 25, 22),
	(29, 26, 20),
	(30, 27, 19),
	(31, 28, 19),
	(32, 29, 19),
	(33, 30, 25)
SET IDENTITY_INSERT [BookAuthor] OFF

--table data for Series
SET IDENTITY_INSERT [Series] ON
INSERT INTO [Series]
	([id], [name])
VALUES
	(1, 'Monk & Robot'),
	(2, 'Enchanted Forest Chronicles'),
	(3, 'Wayfarers'),
	(4, 'The Atlas'),
	(5, 'This World or Any Other'),
	(6, 'Rights and Wrongs'),
	(7, 'Death'),
	(8, 'Discworld')
SET IDENTITY_INSERT [Series] OFF

--table data for SeriesBook
SET IDENTITY_INSERT [SeriesBook] ON
INSERT INTO [SeriesBook]
	([id], [bookId], [seriesId], [position])
VALUES
	(1, 4, 1, 1),
	(2, 7, 2, 1),
	(3, 8, 3, 1),
	(4, 9, 4, 1),
	(5, 11, 4, 2),
	(6, 13, 5, 1),
	(7, 21, 6, 3),
	(8, 22, 6, 2),
	(9, 23, 6, 1),
	(10, 30, 7, 4),
	(11, 30, 8, 20)
SET IDENTITY_INSERT [SeriesBook] OFF