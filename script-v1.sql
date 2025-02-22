USE [master]
GO
/****** Object:  Database [GrowGreen]    Script Date: 13/12/2022 12:58:05 ******/
CREATE DATABASE [GrowGreen]
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
/****** Object:  Table [dbo].[Badge]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[BadgeLearner]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[CarbonHistory]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[CarbonType]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[CarbonTypeHistory]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Chat]    Script Date: 13/12/2022 12:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Content] [nvarchar](280) NOT NULL,
	[IsRead] [bit] NOT NULL,
	[EditedTimestamp] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Chat] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChatReport]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Completed]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Course]    Script Date: 13/12/2022 12:58:05 ******/
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
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseReview]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Donation]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Event]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[GivingReview]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[ItemType]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Lecture]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Newsletter]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[NewsletterEditHIstory]    Script Date: 13/12/2022 12:58:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsletterEditHIstory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[NewsletterId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_NewsletterEditHIstory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[PointsEarned]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Post]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Quiz]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[QuizChoice]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[QuizQuestion]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[QuizResponse]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[RecyclingLocation]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[RecyclingRecord]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[RedeemReward]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[ReportHistory]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Request]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Reward]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[SearchRequest]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[SearchResult]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[SearchResultPost]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Ticket]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Tip]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[Video]    Script Date: 13/12/2022 12:58:05 ******/
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
/****** Object:  Table [dbo].[VideoCompletion]    Script Date: 13/12/2022 12:58:05 ******/
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
ALTER TABLE [dbo].[CourseReview]  WITH CHECK ADD  CONSTRAINT [FK_Review_Course] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Course] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CourseReview] CHECK CONSTRAINT [FK_Review_Course]
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
