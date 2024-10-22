USE [master]
GO
/****** Object:  Database [GrowGreen]    Script Date: 10/1/2023 13:12:03 ******/
CREATE DATABASE [GrowGreen]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GrowGreen', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\GrowGreen.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GrowGreen_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\GrowGreen_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [GrowGreen] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GrowGreen].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GrowGreen] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GrowGreen] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GrowGreen] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GrowGreen] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GrowGreen] SET ARITHABORT OFF 
GO
ALTER DATABASE [GrowGreen] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GrowGreen] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GrowGreen] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GrowGreen] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GrowGreen] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GrowGreen] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GrowGreen] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GrowGreen] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GrowGreen] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GrowGreen] SET  DISABLE_BROKER 
GO
ALTER DATABASE [GrowGreen] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GrowGreen] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GrowGreen] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GrowGreen] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GrowGreen] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GrowGreen] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GrowGreen] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GrowGreen] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GrowGreen] SET  MULTI_USER 
GO
ALTER DATABASE [GrowGreen] SET PAGE_VERIFY NONE  
GO
ALTER DATABASE [GrowGreen] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GrowGreen] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GrowGreen] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GrowGreen] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GrowGreen] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'GrowGreen', N'ON'
GO
ALTER DATABASE [GrowGreen] SET QUERY_STORE = OFF
GO
USE [GrowGreen]
GO
/****** Object:  Table [dbo].[Badge]    Script Date: 10/1/2023 13:12:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Badge](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_Badge] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BadgeLearner]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BadgeLearner](
	[BadgeId] [int] NOT NULL,
	[LearnerId] [int] NOT NULL,
	[Timestamp] [datetime] NULL,
 CONSTRAINT [PK_BadgeLearner] PRIMARY KEY CLUSTERED 
(
	[BadgeId] ASC,
	[LearnerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarbonHistory]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarbonHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[LearnerId] [int] NOT NULL,
 CONSTRAINT [PK_CarbonHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarbonType]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarbonType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[UnitName] [nvarchar](100) NOT NULL,
	[TonsPerUnit] [float] NOT NULL,
 CONSTRAINT [PK_CarbonType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CarbonTypeHistory]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CarbonTypeHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UnitAmount] [float] NOT NULL,
	[CarbonHistoryId] [int] NOT NULL,
	[CarbonTypeId] [int] NOT NULL,
 CONSTRAINT [PK_CarbonTypeHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chat]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Content] [nvarchar](280) NOT NULL,
	[IsReadByLecturer] [bit] NOT NULL,
	[IsReadByUser] [bit] NULL,
	[UserId] [int] NOT NULL,
	[EditedTimestamp] [datetime] NULL,
	[PostId][int] NULL,
	[CourseId] [int] NULL,
	[ReplyToChatId] [int] NULL,
	[AttachmentUrl] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatReport]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChatReport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](280) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[ChatId] [int] NOT NULL,
 CONSTRAINT [PK_ChatReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Completed]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Completed](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[PostId] [int] NOT NULL,
 CONSTRAINT [PK_Completed] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Course]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[LecturerId] [int] NOT NULL,
	[LastUpdatedTimestamp] [datetime] NULL,
	[ImageUrl] [nvarchar](200) NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseReview]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseReview](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [int] NOT NULL,
	[Comment] [nvarchar](200) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseSignup]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseSignup](
	[LearnerId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_CourseSignup] PRIMARY KEY CLUSTERED 
(
	[LearnerId] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Donation]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Remarks] [nvarchar](100) NOT NULL,
	[Amount] [money] NOT NULL,
	[ReceipientId] [int] NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Purpose] [nvarchar](100) NOT NULL,
	[SenderId] [int] NULL,
	[TransactionId] [int] NULL,
 CONSTRAINT [PK_Donation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Email]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Email](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_NewsletterEmail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[MaxParticipants] [int] NOT NULL,
	[Attendee] [nvarchar](200) NOT NULL,
	[Price] [money] NOT NULL,
	[Duration] [time](7) NOT NULL,
	[SpecialGuest] [nvarchar](200) NOT NULL,
	[ActiveStatus] [bit] NOT NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GivingReview]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GivingReview](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Stars] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[SenderId] [int] NOT NULL,
	[PostId] [int] NOT NULL,
 CONSTRAINT [PK_GivingReview] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemType]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ItemType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lecture]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lecture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_Lecture] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Newsletter]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Newsletter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_Newsletter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsletterEditHIstory]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterEditHIstory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Content] [nvarchar](max) NOT NULL,
	[NewsletterId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_NewsletterEditHIstory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsletterEmail]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterEmail](
	[NewsletterId] [int] NOT NULL,
	[EmailId] [int] NOT NULL,
 CONSTRAINT [PK_Table1] PRIMARY KEY CLUSTERED 
(
	[NewsletterId] ASC,
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[TransactionType] [nvarchar](50) NOT NULL,
	[Remarks] [nvarchar](200) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[CardDetails] [nvarchar](max) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PointsEarned]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointsEarned](
	[TransactionId] [int] NOT NULL,
	[Points] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_PointsEarned] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Likes] [int] NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
	[Location] [nvarchar](200) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Quiz]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quiz](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](2000) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[TotalPoints] [int] NOT NULL,
	[LectureId] [int] NOT NULL,
 CONSTRAINT [PK_Quiz] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizChoice]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizChoice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[IsCorrect] [bit] NOT NULL,
	[QuizQuestionId] [int] NOT NULL,
 CONSTRAINT [PK_QuizChoice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizQuestion]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Points] [int] NOT NULL,
	[QuizId] [int] NOT NULL,
 CONSTRAINT [PK_QuizQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuizResponse]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuizResponse](
	[LearnerId] [int] NOT NULL,
	[QuizChoiceId] [int] NOT NULL,
 CONSTRAINT [PK_QuizResponse] PRIMARY KEY CLUSTERED 
(
	[LearnerId] ASC,
	[QuizChoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecyclingLocation]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecyclingLocation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
	[OpeningTime] [time](7) NOT NULL,
	[ClosingTime] [time](7) NOT NULL,
 CONSTRAINT [PK_RecyclingLocation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecyclingRecord]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecyclingRecord](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[RecyclingLocationId] [int] NOT NULL,
	[ItemTypeId] [int] NOT NULL,
	[LearnerId] [int] NOT NULL,
 CONSTRAINT [PK_RecyclingRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RedeemReward]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RedeemReward](
	[RewardId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[PointsDeduction] [int] NOT NULL,
 CONSTRAINT [PK_RedeemRewards] PRIMARY KEY CLUSTERED 
(
	[RewardId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReportHistory]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReportHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Filename] [nvarchar](200) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[AdminId] [int] NOT NULL,
 CONSTRAINT [PK_ReportHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[DateSent] [datetime] NOT NULL,
	[AcceptedStatus] [bit] NOT NULL,
	[PostId] [int] NOT NULL,
 CONSTRAINT [PK_Requiest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reward]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reward](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Likes] [int] NOT NULL,
	[Image] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Reward] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchRequest]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](200) NOT NULL,
	[Popularity] [int] NOT NULL,
	[Keyword] [nvarchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[UserId] [int] NULL,
	[SearchResultId] [int] NULL,
 CONSTRAINT [PK_SearchRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchResult]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MatchedValue] [int] NOT NULL,
	[MatchedPercentage] [float] NOT NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_SearchResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SearchResultPost]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SearchResultPost](
	[SearchResultId] [int] NOT NULL,
	[PostId] [int] NOT NULL,
 CONSTRAINT [PK_SearchResultPost] PRIMARY KEY CLUSTERED 
(
	[SearchResultId] ASC,
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ticket]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ticket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[IsRevoked] [bit] NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tip]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tip](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[ReceiverId] [int] NOT NULL,
	[Amount] [money] NOT NULL,
	[PaymentMethod] [nvarchar](50) NOT NULL,
	[CompletedId] [int] NOT NULL,
	[TransactionId] [int] NOT NULL,
 CONSTRAINT [PK_Tip] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](200) NOT NULL,
	[PhotoFilename] [nvarchar](max) NULL,
	[Phone] [nvarchar](30) NOT NULL,
	[Bio] [nvarchar](max) NOT NULL,
	[SignupTimestamp] [datetime] NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[Qualification] [nvarchar](200) NULL,
	[IsAdmin] [bit] NOT NULL,
	[IsLecturer] [bit] NOT NULL,
	[IsLearner] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Video]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Video](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Transcript] [nvarchar](max) NOT NULL,
	[LectureId] [int] NOT NULL,
 CONSTRAINT [PK_Video] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoCompletion]    Script Date: 10/1/2023 13:12:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoCompletion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[LearnerId] [int] NOT NULL,
	[VideoId] [int] NOT NULL,
 CONSTRAINT [PK_VideoCompletion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Chat] ON 

INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (2, CAST(N'2023-01-10T03:46:26.807' AS DateTime), N'how''s this thing going for everyone?', 1, 1, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (3, CAST(N'2023-01-10T04:08:24.093' AS DateTime), N'Hello!', 1, 1, NULL, 4, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (4, CAST(N'2023-01-10T05:31:45.667' AS DateTime), N'It''s great! Now I understand the importance of trees in the environment hehe', 1, 2, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (7, CAST(N'2023-01-10T06:01:19.853' AS DateTime), N'hewwo', 1, 5, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (9, CAST(N'2023-01-10T06:02:02.927' AS DateTime), N'hi', 1, 5, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (10, CAST(N'2023-01-10T06:02:07.650' AS DateTime), N'sdfds', 1, 5, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (11, CAST(N'2023-01-10T06:02:51.323' AS DateTime), N'hi', 1, 5, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (12, CAST(N'2023-01-10T06:03:35.337' AS DateTime), N'yo', 1, 5, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (13, CAST(N'2023-01-10T06:06:53.393' AS DateTime), N'hi', 1, 2, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (14, CAST(N'2023-01-10T06:07:56.013' AS DateTime), N'Hi students', 1, 6, NULL, 6, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (15, CAST(N'2023-01-10T06:08:06.907' AS DateTime), N'hello', 0, 2, NULL, 6, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (17, CAST(N'2023-01-10T09:27:53.107' AS DateTime), N'hello', 1, 2, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (18, CAST(N'2023-01-10T09:28:05.263' AS DateTime), N'LETS ROCK AND ROLL', 1, 2, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (20, CAST(N'2023-01-10T09:46:34.740' AS DateTime), N'nyom nyom', 0, 2, NULL, 6, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (22, CAST(N'2023-01-10T10:00:51.753' AS DateTime), N'hi', 1, 1, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (23, CAST(N'2023-01-10T10:01:14.317' AS DateTime), N'hello :)', 1, 2, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (24, CAST(N'2023-01-10T10:01:54.803' AS DateTime), N'hello :)', 1, 2, NULL, 1, NULL, NULL)
INSERT [dbo].[Chat] ([Id], [Timestamp], [Content], [IsReadByLecturer], [UserId], [EditedTimestamp], [CourseId], [ReplyToChatId], [AttachmentUrl]) VALUES (25, CAST(N'2023-01-10T10:01:58.227' AS DateTime), N'hello :)hi', 1, 2, NULL, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Chat] OFF
GO
SET IDENTITY_INSERT [dbo].[Course] ON 

INSERT [dbo].[Course] ([Id], [Name], [Description], [StartDate], [EndDate], [LecturerId], [LastUpdatedTimestamp], [ImageUrl]) VALUES (1, N'Importance of Trees in Our Environment', N'This course touches on the importance of trees in our environment. Great for learners of all ages. hehe', CAST(N'2023-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-17T00:00:00.000' AS DateTime), 1, NULL, N'/uploads/b635a0c4-adb0-41d2-9b88-73f62837f96d-sapeksh-singh-siwach-XvyZg4Nh93Y-unsplash.jpg')
INSERT [dbo].[Course] ([Id], [Name], [Description], [StartDate], [EndDate], [LecturerId], [LastUpdatedTimestamp], [ImageUrl]) VALUES (3, N'past course', N'hi', CAST(N'2021-01-09T00:00:00.000' AS DateTime), CAST(N'2022-01-09T00:00:00.000' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Course] ([Id], [Name], [Description], [StartDate], [EndDate], [LecturerId], [LastUpdatedTimestamp], [ImageUrl]) VALUES (4, N'gemini :"D', N'yeet', CAST(N'2023-01-09T00:00:00.000' AS DateTime), CAST(N'2024-01-09T00:00:00.000' AS DateTime), 1, NULL, N'/uploads/courseThumbnail/ece95b34-e60c-46e2-822c-79bdc147876d-Sage-Green-Aesthetic-Wallpaper-2.png')
INSERT [dbo].[Course] ([Id], [Name], [Description], [StartDate], [EndDate], [LecturerId], [LastUpdatedTimestamp], [ImageUrl]) VALUES (6, N'EDP Environment', N'In this course, you will come up with a solution to solve the problems of the Green Plan', CAST(N'2023-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-10T00:00:00.000' AS DateTime), 6, NULL, NULL)
INSERT [dbo].[Course] ([Id], [Name], [Description], [StartDate], [EndDate], [LecturerId], [LastUpdatedTimestamp], [ImageUrl]) VALUES (7, N'hehe', N'ebehehehe', CAST(N'2023-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-10T00:00:00.000' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Course] ([Id], [Name], [Description], [StartDate], [EndDate], [LecturerId], [LastUpdatedTimestamp], [ImageUrl]) VALUES (8, N'test', N'test ', CAST(N'2023-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-30T00:00:00.000' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Course] ([Id], [Name], [Description], [StartDate], [EndDate], [LecturerId], [LastUpdatedTimestamp], [ImageUrl]) VALUES (9, N'test 2', N'hehehehe', CAST(N'2023-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-10T00:00:00.000' AS DateTime), 1, NULL, N'/uploads/courseThumbnail/3c45d49d-9613-4c6a-ac87-fa153ae77f9c-9-Mud Room_Back to Nature S340-4.jpeg')
SET IDENTITY_INSERT [dbo].[Course] OFF
GO
INSERT [dbo].[CourseSignup] ([LearnerId], [CourseId]) VALUES (2, 1)
INSERT [dbo].[CourseSignup] ([LearnerId], [CourseId]) VALUES (2, 3)
INSERT [dbo].[CourseSignup] ([LearnerId], [CourseId]) VALUES (2, 4)
INSERT [dbo].[CourseSignup] ([LearnerId], [CourseId]) VALUES (2, 6)
INSERT [dbo].[CourseSignup] ([LearnerId], [CourseId]) VALUES (2, 7)
INSERT [dbo].[CourseSignup] ([LearnerId], [CourseId]) VALUES (5, 1)
INSERT [dbo].[CourseSignup] ([LearnerId], [CourseId]) VALUES (5, 4)
INSERT [dbo].[CourseSignup] ([LearnerId], [CourseId]) VALUES (5, 6)
INSERT [dbo].[CourseSignup] ([LearnerId], [CourseId]) VALUES (5, 7)
GO
SET IDENTITY_INSERT [dbo].[Email] ON 

INSERT [dbo].[Email] ([Id], [Email]) VALUES (3, N'test@test.com')
INSERT [dbo].[Email] ([Id], [Email]) VALUES (5, N'jjj@gmail.com')
SET IDENTITY_INSERT [dbo].[Email] OFF
GO
SET IDENTITY_INSERT [dbo].[Newsletter] ON 

INSERT [dbo].[Newsletter] ([Id], [Timestamp]) VALUES (1, CAST(N'2023-01-10T09:18:12.367' AS DateTime))
INSERT [dbo].[Newsletter] ([Id], [Timestamp]) VALUES (2, CAST(N'2023-01-10T10:06:47.103' AS DateTime))
INSERT [dbo].[Newsletter] ([Id], [Timestamp]) VALUES (3, CAST(N'2023-01-10T10:07:04.177' AS DateTime))
SET IDENTITY_INSERT [dbo].[Newsletter] OFF
GO
SET IDENTITY_INSERT [dbo].[NewsletterEditHIstory] ON 

INSERT [dbo].[NewsletterEditHIstory] ([Id], [Timestamp], [Description], [Content], [NewsletterId], [UserId]) VALUES (1, CAST(N'2023-01-10T09:18:12.367' AS DateTime), NULL, N'<img src="/uploads/newsletter/c2584018-b300-4fc5-9f3b-221e66230be6-10-6-6k.jpg" />', 1, 7)
INSERT [dbo].[NewsletterEditHIstory] ([Id], [Timestamp], [Description], [Content], [NewsletterId], [UserId]) VALUES (2, CAST(N'2023-01-10T10:06:47.103' AS DateTime), NULL, N'hi', 2, 7)
INSERT [dbo].[NewsletterEditHIstory] ([Id], [Timestamp], [Description], [Content], [NewsletterId], [UserId]) VALUES (3, CAST(N'2023-01-10T10:07:04.177' AS DateTime), NULL, N'<img src="/uploads/newsletter/5e15c2fe-422f-47f1-bbb6-a888e9867e96-10-6-6k.jpg" />', 3, 7)
SET IDENTITY_INSERT [dbo].[NewsletterEditHIstory] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [FullName], [Email], [PhotoFilename], [Phone], [Bio], [SignupTimestamp], [Address], [Password], [Qualification], [IsAdmin], [IsLecturer], [IsLearner]) VALUES (1, N'Viona Erika', N'viona@vyiiona.com', N'sdsf', N'91234567', N'Hi, I am Viona, a lecturer.', CAST(N'2023-01-08T10:59:48.000' AS DateTime), N'Ang mo kio', N'1234', N'University', 0, 1, 0)
INSERT [dbo].[User] ([Id], [FullName], [Email], [PhotoFilename], [Phone], [Bio], [SignupTimestamp], [Address], [Password], [Qualification], [IsAdmin], [IsLecturer], [IsLearner]) VALUES (2, N'Ashlee Tan', N'ash@ashlee.one', N'dfdfl', N'81234567', N'Hi, I am just a learner', CAST(N'2023-01-08T11:01:05.000' AS DateTime), N'Ang mo kio', N'1234', N'Polytechnic', 0, 0, 1)
INSERT [dbo].[User] ([Id], [FullName], [Email], [PhotoFilename], [Phone], [Bio], [SignupTimestamp], [Address], [Password], [Qualification], [IsAdmin], [IsLecturer], [IsLearner]) VALUES (5, N'Trina', N'trina@example.com', N'sdfds', N'61234567', N'Hi, I am just another learner', CAST(N'2023-01-10T06:00:00.000' AS DateTime), N'Ang Mo kio ave 5', N'1234', N'University', 0, 0, 1)
INSERT [dbo].[User] ([Id], [FullName], [Email], [PhotoFilename], [Phone], [Bio], [SignupTimestamp], [Address], [Password], [Qualification], [IsAdmin], [IsLecturer], [IsLearner]) VALUES (6, N'Ms Li', N'msli@example.com', N'sdkljf', N'6234567', N'Hi, I am Ms Li, a lecturer', CAST(N'2023-01-10T06:00:00.000' AS DateTime), N'Ang mo kio ave 5', N'1234', N'University', 0, 1, 0)
INSERT [dbo].[User] ([Id], [FullName], [Email], [PhotoFilename], [Phone], [Bio], [SignupTimestamp], [Address], [Password], [Qualification], [IsAdmin], [IsLecturer], [IsLearner]) VALUES (7, N'Mr Bobby', N'bobbyliu@example.com', N'sdkfljs', N'65501234', N'Hi, I am Mr Bobby, an admin of Grow Green.', CAST(N'2023-01-10T00:00:00.000' AS DateTime), N'Ang Mo Kio Ave 5', N'1234', N'University', 1, 0, 0)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Course]    Script Date: 10/1/2023 13:12:17 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Course] ON [dbo].[Course]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chat] ADD  CONSTRAINT [DF_Chat_IsReadByLecturer]  DEFAULT ((0)) FOR [IsReadByLecturer]
GO
ALTER TABLE [dbo].[ChatReport] ADD  CONSTRAINT [DF_ChatReport_IsRead]  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsAdmin]  DEFAULT ((0)) FOR [IsAdmin]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsLecturer]  DEFAULT ((0)) FOR [IsLecturer]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsLearner]  DEFAULT ((0)) FOR [IsLearner]
GO
ALTER TABLE [dbo].[Badge]  WITH CHECK ADD  CONSTRAINT [FK_Badge_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Badge] CHECK CONSTRAINT [FK_Badge_Course]
GO
ALTER TABLE [dbo].[BadgeLearner]  WITH CHECK ADD  CONSTRAINT [FK_BadgeLearner_Badge] FOREIGN KEY([BadgeId])
REFERENCES [dbo].[Badge] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BadgeLearner] CHECK CONSTRAINT [FK_BadgeLearner_Badge]
GO
ALTER TABLE [dbo].[BadgeLearner]  WITH CHECK ADD  CONSTRAINT [FK_BadgeLearner_User] FOREIGN KEY([LearnerId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BadgeLearner] CHECK CONSTRAINT [FK_BadgeLearner_User]
GO
ALTER TABLE [dbo].[CarbonHistory]  WITH CHECK ADD  CONSTRAINT [FK_CarbonHistory_User] FOREIGN KEY([LearnerId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CarbonHistory] CHECK CONSTRAINT [FK_CarbonHistory_User]
GO
ALTER TABLE [dbo].[CarbonTypeHistory]  WITH CHECK ADD  CONSTRAINT [FK_CarbonTypeHistory_CarbonHistory] FOREIGN KEY([CarbonHistoryId])
REFERENCES [dbo].[CarbonHistory] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CarbonTypeHistory] CHECK CONSTRAINT [FK_CarbonTypeHistory_CarbonHistory]
GO
ALTER TABLE [dbo].[CarbonTypeHistory]  WITH CHECK ADD  CONSTRAINT [FK_CarbonTypeHistory_CarbonType] FOREIGN KEY([CarbonTypeId])
REFERENCES [dbo].[CarbonType] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CarbonTypeHistory] CHECK CONSTRAINT [FK_CarbonTypeHistory_CarbonType]
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Chat] FOREIGN KEY([ReplyToChatId])
REFERENCES [dbo].[Chat] ([Id])
GO
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Chat]
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_Course]
GO
ALTER TABLE [dbo].[Chat]  WITH CHECK ADD  CONSTRAINT [FK_Chat_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Chat] CHECK CONSTRAINT [FK_Chat_User]
GO
ALTER TABLE [dbo].[ChatReport]  WITH CHECK ADD  CONSTRAINT [FK_ChatReport_Chat] FOREIGN KEY([ChatId])
REFERENCES [dbo].[Chat] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ChatReport] CHECK CONSTRAINT [FK_ChatReport_Chat]
GO
ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_User] FOREIGN KEY([LecturerId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_User]
GO
ALTER TABLE [dbo].[CourseReview]  WITH CHECK ADD  CONSTRAINT [FK_Review_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseReview] CHECK CONSTRAINT [FK_Review_Course]
GO
ALTER TABLE [dbo].[CourseSignup]  WITH CHECK ADD  CONSTRAINT [FK_CourseSignup_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseSignup] CHECK CONSTRAINT [FK_CourseSignup_Course]
GO
ALTER TABLE [dbo].[CourseSignup]  WITH CHECK ADD  CONSTRAINT [FK_CourseSignup_User] FOREIGN KEY([LearnerId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseSignup] CHECK CONSTRAINT [FK_CourseSignup_User]
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD  CONSTRAINT [FK_Donation_Payment] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[Payment] ([TransactionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Donation] CHECK CONSTRAINT [FK_Donation_Payment]
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD  CONSTRAINT [FK_Donation_User] FOREIGN KEY([SenderId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Donation] CHECK CONSTRAINT [FK_Donation_User]
GO
ALTER TABLE [dbo].[GivingReview]  WITH CHECK ADD  CONSTRAINT [FK_GivingReview_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([PostId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GivingReview] CHECK CONSTRAINT [FK_GivingReview_Post]
GO
ALTER TABLE [dbo].[Lecture]  WITH CHECK ADD  CONSTRAINT [FK_Lecture_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Lecture] CHECK CONSTRAINT [FK_Lecture_Course]
GO
ALTER TABLE [dbo].[NewsletterEditHIstory]  WITH CHECK ADD  CONSTRAINT [FK_NewsletterEditHIstory_Newsletter] FOREIGN KEY([NewsletterId])
REFERENCES [dbo].[Newsletter] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsletterEditHIstory] CHECK CONSTRAINT [FK_NewsletterEditHIstory_Newsletter]
GO
ALTER TABLE [dbo].[NewsletterEditHIstory]  WITH CHECK ADD  CONSTRAINT [FK_NewsletterEditHIstory_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsletterEditHIstory] CHECK CONSTRAINT [FK_NewsletterEditHIstory_User]
GO
ALTER TABLE [dbo].[NewsletterEmail]  WITH CHECK ADD  CONSTRAINT [FK_NewsletterEmail_Email] FOREIGN KEY([EmailId])
REFERENCES [dbo].[Email] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsletterEmail] CHECK CONSTRAINT [FK_NewsletterEmail_Email]
GO
ALTER TABLE [dbo].[NewsletterEmail]  WITH CHECK ADD  CONSTRAINT [FK_NewsletterEmail_Newsletter] FOREIGN KEY([NewsletterId])
REFERENCES [dbo].[Newsletter] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[NewsletterEmail] CHECK CONSTRAINT [FK_NewsletterEmail_Newsletter]
GO
ALTER TABLE [dbo].[PointsEarned]  WITH CHECK ADD  CONSTRAINT [FK_PointsEarned_User] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PointsEarned] CHECK CONSTRAINT [FK_PointsEarned_User]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_User]
GO
ALTER TABLE [dbo].[Quiz]  WITH CHECK ADD  CONSTRAINT [FK_Quiz_Lecture] FOREIGN KEY([LectureId])
REFERENCES [dbo].[Lecture] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Quiz] CHECK CONSTRAINT [FK_Quiz_Lecture]
GO
ALTER TABLE [dbo].[QuizChoice]  WITH CHECK ADD  CONSTRAINT [FK_QuizChoice_QuizQuestion] FOREIGN KEY([QuizQuestionId])
REFERENCES [dbo].[QuizQuestion] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuizChoice] CHECK CONSTRAINT [FK_QuizChoice_QuizQuestion]
GO
ALTER TABLE [dbo].[QuizQuestion]  WITH CHECK ADD  CONSTRAINT [FK_QuizQuestion_Quiz] FOREIGN KEY([QuizId])
REFERENCES [dbo].[Quiz] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuizQuestion] CHECK CONSTRAINT [FK_QuizQuestion_Quiz]
GO
ALTER TABLE [dbo].[QuizResponse]  WITH CHECK ADD  CONSTRAINT [FK_QuizResponse_QuizChoice] FOREIGN KEY([QuizChoiceId])
REFERENCES [dbo].[QuizChoice] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuizResponse] CHECK CONSTRAINT [FK_QuizResponse_QuizChoice]
GO
ALTER TABLE [dbo].[QuizResponse]  WITH CHECK ADD  CONSTRAINT [FK_QuizResponse_User] FOREIGN KEY([LearnerId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QuizResponse] CHECK CONSTRAINT [FK_QuizResponse_User]
GO
ALTER TABLE [dbo].[RecyclingRecord]  WITH CHECK ADD  CONSTRAINT [FK_RecyclingRecord_ItemType] FOREIGN KEY([ItemTypeId])
REFERENCES [dbo].[ItemType] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecyclingRecord] CHECK CONSTRAINT [FK_RecyclingRecord_ItemType]
GO
ALTER TABLE [dbo].[RecyclingRecord]  WITH CHECK ADD  CONSTRAINT [FK_RecyclingRecord_RecyclingLocation] FOREIGN KEY([RecyclingLocationId])
REFERENCES [dbo].[RecyclingLocation] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RecyclingRecord] CHECK CONSTRAINT [FK_RecyclingRecord_RecyclingLocation]
GO
ALTER TABLE [dbo].[RecyclingRecord]  WITH CHECK ADD  CONSTRAINT [FK_RecyclingRecord_User] FOREIGN KEY([LearnerId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[RecyclingRecord] CHECK CONSTRAINT [FK_RecyclingRecord_User]
GO
ALTER TABLE [dbo].[RedeemReward]  WITH CHECK ADD  CONSTRAINT [FK_RedeemReward_Reward] FOREIGN KEY([RewardId])
REFERENCES [dbo].[Reward] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RedeemReward] CHECK CONSTRAINT [FK_RedeemReward_Reward]
GO
ALTER TABLE [dbo].[RedeemReward]  WITH CHECK ADD  CONSTRAINT [FK_RedeemReward_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RedeemReward] CHECK CONSTRAINT [FK_RedeemReward_User]
GO
ALTER TABLE [dbo].[ReportHistory]  WITH CHECK ADD  CONSTRAINT [FK_ReportHistory_User] FOREIGN KEY([AdminId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReportHistory] CHECK CONSTRAINT [FK_ReportHistory_User]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([PostId])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Post]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_User] FOREIGN KEY([SenderId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_User]
GO
ALTER TABLE [dbo].[SearchRequest]  WITH CHECK ADD  CONSTRAINT [FK_SearchRequest_SearchResult] FOREIGN KEY([SearchResultId])
REFERENCES [dbo].[SearchResult] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SearchRequest] CHECK CONSTRAINT [FK_SearchRequest_SearchResult]
GO
ALTER TABLE [dbo].[SearchRequest]  WITH CHECK ADD  CONSTRAINT [FK_SearchRequest_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SearchRequest] CHECK CONSTRAINT [FK_SearchRequest_User]
GO
ALTER TABLE [dbo].[SearchResultPost]  WITH CHECK ADD  CONSTRAINT [FK_SearchResultPost_Post] FOREIGN KEY([PostId])
REFERENCES [dbo].[Post] ([PostId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SearchResultPost] CHECK CONSTRAINT [FK_SearchResultPost_Post]
GO
ALTER TABLE [dbo].[SearchResultPost]  WITH CHECK ADD  CONSTRAINT [FK_SearchResultPost_SearchResult] FOREIGN KEY([SearchResultId])
REFERENCES [dbo].[SearchResult] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SearchResultPost] CHECK CONSTRAINT [FK_SearchResultPost_SearchResult]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_Event]
GO
ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_User]
GO
ALTER TABLE [dbo].[Tip]  WITH CHECK ADD  CONSTRAINT [FK_Tip_Completed] FOREIGN KEY([CompletedId])
REFERENCES [dbo].[Completed] ([Id])
GO
ALTER TABLE [dbo].[Tip] CHECK CONSTRAINT [FK_Tip_Completed]
GO
ALTER TABLE [dbo].[Tip]  WITH CHECK ADD  CONSTRAINT [FK_Tip_Payment] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[Payment] ([TransactionId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tip] CHECK CONSTRAINT [FK_Tip_Payment]
GO
ALTER TABLE [dbo].[Video]  WITH CHECK ADD  CONSTRAINT [FK_Video_Lecture] FOREIGN KEY([LectureId])
REFERENCES [dbo].[Lecture] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Video] CHECK CONSTRAINT [FK_Video_Lecture]
GO
ALTER TABLE [dbo].[VideoCompletion]  WITH CHECK ADD  CONSTRAINT [FK_VideoCompletion_User] FOREIGN KEY([LearnerId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VideoCompletion] CHECK CONSTRAINT [FK_VideoCompletion_User]
GO
ALTER TABLE [dbo].[VideoCompletion]  WITH CHECK ADD  CONSTRAINT [FK_VideoCompletion_Video] FOREIGN KEY([VideoId])
REFERENCES [dbo].[Video] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VideoCompletion] CHECK CONSTRAINT [FK_VideoCompletion_Video]
GO
USE [master]
GO
ALTER DATABASE [GrowGreen] SET  READ_WRITE 
GO
