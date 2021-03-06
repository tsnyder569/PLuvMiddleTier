USE [POC1]
GO
/****** Object:  Index [SpatialIndex-20160415-153447]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Location]') AND name = N'SpatialIndex-20160415-153447')
DROP INDEX [SpatialIndex-20160415-153447] ON [dbo].[Location]
GO
IF  EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Ref_ResponseType', NULL,NULL))
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Ref_ResponseType'

GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses] DROP CONSTRAINT [FK_UserResponses_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_Responses]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses] DROP CONSTRAINT [FK_UserResponses_Responses]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_Ref_ResponseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses] DROP CONSTRAINT [FK_UserResponses_Ref_ResponseType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_Ref_QuestionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses] DROP CONSTRAINT [FK_UserResponses_Ref_QuestionType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Responses_Ref_ResponseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Responses]'))
ALTER TABLE [dbo].[Responses] DROP CONSTRAINT [FK_Responses_Ref_ResponseType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfileChocies_User_02]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfileChocies]'))
ALTER TABLE [dbo].[ProfileChocies] DROP CONSTRAINT [FK_ProfileChocies_User_02]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfileChocies_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfileChocies]'))
ALTER TABLE [dbo].[ProfileChocies] DROP CONSTRAINT [FK_ProfileChocies_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfileChocies_Ref_MatchType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfileChocies]'))
ALTER TABLE [dbo].[ProfileChocies] DROP CONSTRAINT [FK_ProfileChocies_Ref_MatchType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Photo_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Photo]'))
ALTER TABLE [dbo].[Photo] DROP CONSTRAINT [FK_Photo_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Photo_Ref_PhotoType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Photo]'))
ALTER TABLE [dbo].[Photo] DROP CONSTRAINT [FK_Photo_Ref_PhotoType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] DROP CONSTRAINT [FK_Location_User]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attributes_Ref_ResponseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attributes]'))
ALTER TABLE [dbo].[Attributes] DROP CONSTRAINT [FK_Attributes_Ref_ResponseType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attributes_Ref_QuestionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attributes]'))
ALTER TABLE [dbo].[Attributes] DROP CONSTRAINT [FK_Attributes_Ref_QuestionType]
GO
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__Photo__IsPrimary__278EDA44]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Photo] DROP CONSTRAINT [DF__Photo__IsPrimary__278EDA44]
END

GO
/****** Object:  Index [IXFK_UserResponses_User]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserResponses]') AND name = N'IXFK_UserResponses_User')
DROP INDEX [IXFK_UserResponses_User] ON [dbo].[UserResponses]
GO
/****** Object:  Index [IXFK_UserResponses_Ref_QuestionType]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserResponses]') AND name = N'IXFK_UserResponses_Ref_QuestionType')
DROP INDEX [IXFK_UserResponses_Ref_QuestionType] ON [dbo].[UserResponses]
GO
/****** Object:  Index [IXFK_ProfileChocies_User]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProfileChocies]') AND name = N'IXFK_ProfileChocies_User')
DROP INDEX [IXFK_ProfileChocies_User] ON [dbo].[ProfileChocies]
GO
/****** Object:  Index [IXFK_ProfileChocies_Ref_MatchType]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProfileChocies]') AND name = N'IXFK_ProfileChocies_Ref_MatchType')
DROP INDEX [IXFK_ProfileChocies_Ref_MatchType] ON [dbo].[ProfileChocies]
GO
/****** Object:  Index [IXFK_Attributes_Ref_QuestionType]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Attributes]') AND name = N'IXFK_Attributes_Ref_QuestionType')
DROP INDEX [IXFK_Attributes_Ref_QuestionType] ON [dbo].[Attributes]
GO
/****** Object:  Table [dbo].[UserResponses]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserResponses]') AND type in (N'U'))
DROP TABLE [dbo].[UserResponses]
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Responses]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Responses]') AND type in (N'U'))
DROP TABLE [dbo].[Responses]
GO
/****** Object:  Table [dbo].[Ref_ResponseType]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_ResponseType]') AND type in (N'U'))
DROP TABLE [dbo].[Ref_ResponseType]
GO
/****** Object:  Table [dbo].[Ref_QuestionType]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_QuestionType]') AND type in (N'U'))
DROP TABLE [dbo].[Ref_QuestionType]
GO
/****** Object:  Table [dbo].[Ref_PhotoType]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_PhotoType]') AND type in (N'U'))
DROP TABLE [dbo].[Ref_PhotoType]
GO
/****** Object:  Table [dbo].[Ref_Config]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_Config]') AND type in (N'U'))
DROP TABLE [dbo].[Ref_Config]
GO
/****** Object:  Table [dbo].[Ref_ChoiceType]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_ChoiceType]') AND type in (N'U'))
DROP TABLE [dbo].[Ref_ChoiceType]
GO
/****** Object:  Table [dbo].[ProfileChocies]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProfileChocies]') AND type in (N'U'))
DROP TABLE [dbo].[ProfileChocies]
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photo]') AND type in (N'U'))
DROP TABLE [dbo].[Photo]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Location]') AND type in (N'U'))
DROP TABLE [dbo].[Location]
GO
/****** Object:  Table [dbo].[Attributes]    Script Date: 5/11/2016 7:43:53 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Attributes]') AND type in (N'U'))
DROP TABLE [dbo].[Attributes]
GO
/****** Object:  Table [dbo].[Attributes]    Script Date: 5/11/2016 7:43:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Attributes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Attributes](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Question] [varchar](500) NOT NULL,
	[QuestionTypeID] [bigint] NOT NULL,
	[ResponseTypeID] [bigint] NOT NULL,
 CONSTRAINT [PK_Attribute] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Attributes] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Location]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Location]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Location](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[Latitude] [numeric](18, 15) NOT NULL,
	[Longitude] [numeric](18, 15) NOT NULL,
	[Accuracy] [numeric](6, 0) NOT NULL,
	[TimeStamp] [numeric](13, 0) NOT NULL,
	[SpatialLocation] [geography] NOT NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_Location_User] UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Location] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Photo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Photo](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PhotoImage] [nvarchar](max) NOT NULL,
	[PhotoTypeID] [bigint] NOT NULL,
	[UserID] [bigint] NOT NULL,
	[IsPrimary] [bit] NOT NULL,
 CONSTRAINT [PK_Photo] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[Photo] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[ProfileChocies]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProfileChocies]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProfileChocies](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[ChoiceType] [bigint] NOT NULL,
	[ProfileChoiceUserID] [bigint] NOT NULL,
 CONSTRAINT [PK_ProfileChoices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_ProfileChocies_User] UNIQUE NONCLUSTERED 
(
	[ProfileChoiceUserID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[ProfileChocies] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Ref_ChoiceType]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_ChoiceType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ref_ChoiceType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[MatchType] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Ref_MatchType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Ref_ChoiceType] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Ref_Config]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_Config]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ref_Config](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Value] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Ref_Config] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Idx_Ref_Config_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Ref_Config] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Ref_PhotoType]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_PhotoType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ref_PhotoType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[PhotoType] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Ref_PhotoType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [Unique_Ref_PhotoType_PhotoType] UNIQUE NONCLUSTERED 
(
	[PhotoType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Ref_PhotoType] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Ref_QuestionType]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_QuestionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ref_QuestionType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[QuestionType] [varchar](500) NOT NULL,
 CONSTRAINT [PK_RefQuestionType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Ref_QuestionType] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Ref_ResponseType]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ref_ResponseType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ref_ResponseType](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ResponseType] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Ref_ResponseType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Ref_ResponseType] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[Responses]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Responses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Responses](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Responses] [varchar](500) NOT NULL,
	[ResponseTypeID] [bigint] NOT NULL,
 CONSTRAINT [PK_Responses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[Responses] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[User]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User](
	[ID] [bigint] NOT NULL,
	[UserName] [varchar](500) NOT NULL,
	[FirstName] [varchar](500) NOT NULL,
	[LastName] [varchar](500) NOT NULL,
	[Email] [varchar](500) NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
	[DisplayName] [varchar](1000) NOT NULL,
	[Birthday] [date] NULL,
	[MiddleName] [varchar](500) NULL,
	[Bio] [nvarchar](max) NULL,
	[Photo] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_UserName] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
ALTER AUTHORIZATION ON [dbo].[User] TO  SCHEMA OWNER 
GO
/****** Object:  Table [dbo].[UserResponses]    Script Date: 5/11/2016 7:43:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserResponses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserResponses](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[UserID] [bigint] NOT NULL,
	[ResponseID] [bigint] NOT NULL,
	[QuestionTypeID] [bigint] NOT NULL,
	[ResponseTypeID] [bigint] NOT NULL,
 CONSTRAINT [PK_UserResponses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_UserResponses_Responses] UNIQUE NONCLUSTERED 
(
	[ResponseID] ASC,
	[UserID] ASC,
	[QuestionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
ALTER AUTHORIZATION ON [dbo].[UserResponses] TO  SCHEMA OWNER 
GO
/****** Object:  Index [IXFK_Attributes_Ref_QuestionType]    Script Date: 5/11/2016 7:43:54 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Attributes]') AND name = N'IXFK_Attributes_Ref_QuestionType')
CREATE NONCLUSTERED INDEX [IXFK_Attributes_Ref_QuestionType] ON [dbo].[Attributes]
(
	[QuestionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_ProfileChocies_Ref_MatchType]    Script Date: 5/11/2016 7:43:54 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProfileChocies]') AND name = N'IXFK_ProfileChocies_Ref_MatchType')
CREATE NONCLUSTERED INDEX [IXFK_ProfileChocies_Ref_MatchType] ON [dbo].[ProfileChocies]
(
	[ChoiceType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_ProfileChocies_User]    Script Date: 5/11/2016 7:43:54 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ProfileChocies]') AND name = N'IXFK_ProfileChocies_User')
CREATE NONCLUSTERED INDEX [IXFK_ProfileChocies_User] ON [dbo].[ProfileChocies]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_UserResponses_Ref_QuestionType]    Script Date: 5/11/2016 7:43:54 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserResponses]') AND name = N'IXFK_UserResponses_Ref_QuestionType')
CREATE NONCLUSTERED INDEX [IXFK_UserResponses_Ref_QuestionType] ON [dbo].[UserResponses]
(
	[QuestionTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IXFK_UserResponses_User]    Script Date: 5/11/2016 7:43:54 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[UserResponses]') AND name = N'IXFK_UserResponses_User')
CREATE NONCLUSTERED INDEX [IXFK_UserResponses_User] ON [dbo].[UserResponses]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF__Photo__IsPrimary__278EDA44]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Photo] ADD  DEFAULT ((0)) FOR [IsPrimary]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attributes_Ref_QuestionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attributes]'))
ALTER TABLE [dbo].[Attributes]  WITH CHECK ADD  CONSTRAINT [FK_Attributes_Ref_QuestionType] FOREIGN KEY([QuestionTypeID])
REFERENCES [dbo].[Ref_QuestionType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attributes_Ref_QuestionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attributes]'))
ALTER TABLE [dbo].[Attributes] CHECK CONSTRAINT [FK_Attributes_Ref_QuestionType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attributes_Ref_ResponseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attributes]'))
ALTER TABLE [dbo].[Attributes]  WITH CHECK ADD  CONSTRAINT [FK_Attributes_Ref_ResponseType] FOREIGN KEY([ResponseTypeID])
REFERENCES [dbo].[Ref_ResponseType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Attributes_Ref_ResponseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Attributes]'))
ALTER TABLE [dbo].[Attributes] CHECK CONSTRAINT [FK_Attributes_Ref_ResponseType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Location_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Location]'))
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Photo_Ref_PhotoType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Photo]'))
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_Ref_PhotoType] FOREIGN KEY([PhotoTypeID])
REFERENCES [dbo].[Ref_PhotoType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Photo_Ref_PhotoType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Photo]'))
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_Ref_PhotoType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Photo_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Photo]'))
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD  CONSTRAINT [FK_Photo_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Photo_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[Photo]'))
ALTER TABLE [dbo].[Photo] CHECK CONSTRAINT [FK_Photo_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfileChocies_Ref_MatchType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfileChocies]'))
ALTER TABLE [dbo].[ProfileChocies]  WITH CHECK ADD  CONSTRAINT [FK_ProfileChocies_Ref_MatchType] FOREIGN KEY([ChoiceType])
REFERENCES [dbo].[Ref_ChoiceType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfileChocies_Ref_MatchType]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfileChocies]'))
ALTER TABLE [dbo].[ProfileChocies] CHECK CONSTRAINT [FK_ProfileChocies_Ref_MatchType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfileChocies_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfileChocies]'))
ALTER TABLE [dbo].[ProfileChocies]  WITH CHECK ADD  CONSTRAINT [FK_ProfileChocies_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfileChocies_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfileChocies]'))
ALTER TABLE [dbo].[ProfileChocies] CHECK CONSTRAINT [FK_ProfileChocies_User]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfileChocies_User_02]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfileChocies]'))
ALTER TABLE [dbo].[ProfileChocies]  WITH CHECK ADD  CONSTRAINT [FK_ProfileChocies_User_02] FOREIGN KEY([ProfileChoiceUserID])
REFERENCES [dbo].[User] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ProfileChocies_User_02]') AND parent_object_id = OBJECT_ID(N'[dbo].[ProfileChocies]'))
ALTER TABLE [dbo].[ProfileChocies] CHECK CONSTRAINT [FK_ProfileChocies_User_02]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Responses_Ref_ResponseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Responses]'))
ALTER TABLE [dbo].[Responses]  WITH CHECK ADD  CONSTRAINT [FK_Responses_Ref_ResponseType] FOREIGN KEY([ResponseTypeID])
REFERENCES [dbo].[Ref_ResponseType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Responses_Ref_ResponseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Responses]'))
ALTER TABLE [dbo].[Responses] CHECK CONSTRAINT [FK_Responses_Ref_ResponseType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_Ref_QuestionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses]  WITH CHECK ADD  CONSTRAINT [FK_UserResponses_Ref_QuestionType] FOREIGN KEY([QuestionTypeID])
REFERENCES [dbo].[Ref_QuestionType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_Ref_QuestionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses] CHECK CONSTRAINT [FK_UserResponses_Ref_QuestionType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_Ref_ResponseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses]  WITH CHECK ADD  CONSTRAINT [FK_UserResponses_Ref_ResponseType] FOREIGN KEY([ResponseTypeID])
REFERENCES [dbo].[Ref_ResponseType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_Ref_ResponseType]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses] CHECK CONSTRAINT [FK_UserResponses_Ref_ResponseType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_Responses]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses]  WITH CHECK ADD  CONSTRAINT [FK_UserResponses_Responses] FOREIGN KEY([ResponseID])
REFERENCES [dbo].[Responses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_Responses]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses] CHECK CONSTRAINT [FK_UserResponses_Responses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses]  WITH CHECK ADD  CONSTRAINT [FK_UserResponses_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserResponses_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserResponses]'))
ALTER TABLE [dbo].[UserResponses] CHECK CONSTRAINT [FK_UserResponses_User]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Ref_ResponseType', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AgeRange PetType GenderType RelationshipType DistanceType' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Ref_ResponseType'
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
/****** Object:  Index [SpatialIndex-20160415-153447]    Script Date: 5/11/2016 7:43:54 AM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Location]') AND name = N'SpatialIndex-20160415-153447')
CREATE SPATIAL INDEX [SpatialIndex-20160415-153447] ON [dbo].[Location]
(
	[SpatialLocation]
)USING  GEOGRAPHY_GRID 
WITH (GRIDS =(LEVEL_1 = MEDIUM,LEVEL_2 = MEDIUM,LEVEL_3 = MEDIUM,LEVEL_4 = MEDIUM), 
CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
