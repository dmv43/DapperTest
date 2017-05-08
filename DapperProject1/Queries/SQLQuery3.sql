


IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[Teacher]') 
		AND type in (N'U'))
		BEGIN
CREATE TABLE Teacher(
     [id]  [int] IDENTITY (1 , 1)   ,
	[nickname] [nvarchar](70) NULL,
	[italki_url] [nvarchar](70) NULL,
	[min_price] [nvarchar](50) NULL,
	[student_count] [int] NULL,
	[session_count] [int] NULL,
	[description] [ntext] NULL,
	[rating] [nvarchar](20) NULL,
	[country] [nvarchar](50)  NULL,
	[url] [nvarchar](150)  NULL,
	[italki_id] [int]  NULL
	
	)
	
	ALTER TABLE Teacher
ADD CONSTRAINT PK_Teacher  PRIMARY KEY  (id )  ;
IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[Language]') 
		AND type in (N'U'))
	CREATE TABLE [Language] (
	 [id] [int] IDENTITY (1 , 1),
     [language] [nvarchar](50) NOT NULL,
	)
	
	ALTER TABLE [Language]
ADD CONSTRAINT PK_Language PRIMARY KEY (id);
IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[Tag]') 
		AND type in (N'U'))
     CREATE TABLE Tag(
	 [id] [int] IDENTITY (1 , 1),
     [tag] [nvarchar](50) NOT NULL,
	)
	
	ALTER TABLE Tag
ADD CONSTRAINT PK_Tag  PRIMARY KEY  (id)  ;

	CREATE TABLE TeacherLanguage(
	 [teacher_id] [int] NOT NULL,
	 [language_id] [int] NOT NULL,
	
	)
	IF NOT EXISTS (SELECT * FROM sys.objects 
		WHERE object_id = OBJECT_ID(N'[TeacherTag]') 
		AND type in (N'U'))
	CREATE TABLE TeacherTag(
	 [teacher_id] [int] NOT NULL,
	 [tag_id] [int] NOT NULL,
	
	)
	
	ALTER TABLE [TeacherLanguage]  WITH CHECK ADD  
       CONSTRAINT [FK_TeacherLanguage_Teacher] FOREIGN KEY([teacher_id])
REFERENCES [Teacher] ([id])

ALTER TABLE [TeacherLanguage]  WITH CHECK ADD  
       CONSTRAINT [FK_TeacherLanguage_Language] FOREIGN KEY([language_id])
REFERENCES [Language] ([id])

	ALTER TABLE [TeacherTag]  WITH CHECK ADD  
       CONSTRAINT [FK_TeacherTag_Teacher] FOREIGN KEY([teacher_id])
REFERENCES [Teacher] ([id])

	ALTER TABLE [TeacherTag]  WITH CHECK ADD  
       CONSTRAINT [FK_TeacherTag_Tag] FOREIGN KEY([tag_id])
REFERENCES [Tag] ([id])
END
