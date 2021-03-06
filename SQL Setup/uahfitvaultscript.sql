USE [master]
GO
ALTER DATABASE [uahfitvault] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [uahfitvault].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [uahfitvault] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [uahfitvault] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [uahfitvault] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [uahfitvault] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [uahfitvault] SET ARITHABORT OFF 
GO
ALTER DATABASE [uahfitvault] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [uahfitvault] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [uahfitvault] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [uahfitvault] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [uahfitvault] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [uahfitvault] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [uahfitvault] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [uahfitvault] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [uahfitvault] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [uahfitvault] SET  ENABLE_BROKER 
GO
ALTER DATABASE [uahfitvault] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [uahfitvault] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [uahfitvault] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [uahfitvault] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [uahfitvault] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [uahfitvault] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [uahfitvault] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [uahfitvault] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [uahfitvault] SET  MULTI_USER 
GO
ALTER DATABASE [uahfitvault] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [uahfitvault] SET DB_CHAINING OFF 
GO
ALTER DATABASE [uahfitvault] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [uahfitvault] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [uahfitvault] SET DELAYED_DURABILITY = DISABLED 
GO
USE [uahfitvault]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AccountRequests]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReasonForAccount] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AccountRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Activities]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataActivity] [int] NOT NULL,
	[PatientData_Id] [nvarchar](128) NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Activities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Status] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
	[PhysicianId] [int] NOT NULL,
	[ExperimentAdministratorId] [int] NOT NULL,
	[AccountRequestId] [int] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BasisPeakSummaryDatas]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BasisPeakSummaryDatas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Calories] [real] NULL,
	[GSR] [real] NULL,
	[HeartRate] [int] NULL,
	[SkinTemp] [real] NULL,
	[Steps] [int] NOT NULL,
	[PatientDataId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.BasisPeakSummaryDatas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ExperimentAdministrators]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperimentAdministrators](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ExperimentAdministrators] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Experiments]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Experiments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[LastModified] [datetime] NOT NULL,
	[QueryString] [nvarchar](max) NOT NULL,
	[ExperimentAdministrator_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Experiments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MedicalDevices]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MedicalDevices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MedicalDevices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MSBandAccelerometers]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSBandAccelerometers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Vertical] [real] NOT NULL,
	[Lateral] [real] NOT NULL,
	[Sagittal] [real] NOT NULL,
	[PatientDataId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MSBandAccelerometers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MSBandCalories]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSBandCalories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Total] [int] NOT NULL,
	[PatientDataId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MSBandCalories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MSBandDistances]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSBandDistances](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[MotionType] [nvarchar](max) NOT NULL,
	[Pace] [real] NOT NULL,
	[Speed] [real] NOT NULL,
	[Total] [real] NOT NULL,
	[PatientDataId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MSBandDistances] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MSBandGyroscopes]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSBandGyroscopes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
	[X] [real] NOT NULL,
	[Y] [real] NOT NULL,
	[Z] [real] NOT NULL,
	[PatientDataId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MSBandGyroscopes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MSBandHeartRates]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSBandHeartRates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[ReadStatus] [nvarchar](max) NOT NULL,
	[HeartRate] [int] NOT NULL,
	[PatientDataId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MSBandHeartRates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MSBandPedometers]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSBandPedometers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Steps] [int] NOT NULL,
	[PatientDataId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MSBandPedometers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MSBandTemperatures]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSBandTemperatures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Temperature] [real] NOT NULL,
	[PatientDataId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MSBandTemperatures] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MSBandUVs]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MSBandUVs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[UVIndex] [int] NOT NULL,
	[PatientDataId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.MSBandUVs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PatientDatas]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientDatas](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[UploadDate] [datetime] NOT NULL,
	[DataType] [int] NOT NULL,
	[MedicalDeviceId] [int] NOT NULL,
	[Patient_Id] [int] NOT NULL,
	[FromDate] [datetime] NOT NULL,
	[ToDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.PatientDatas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patients]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Weight] [real] NOT NULL,
	[Height] [int] NOT NULL,
	[Location] [int] NOT NULL,
	[Ethnicity] [int] NOT NULL,
	[Race] [int] NOT NULL,
	[Gender] [int] NOT NULL,
	[Physician_Id] [int] NULL,
	[Birthdate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Patients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Physicians]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Physicians](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Physicians] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ZephyrAccelerometers]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZephyrAccelerometers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Vertical] [int] NOT NULL,
	[Lateral] [int] NOT NULL,
	[Sagittal] [int] NOT NULL,
	[PatientDataId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.ZephyrAccelerometers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ZephyrBreathingWaveforms]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZephyrBreathingWaveforms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Data] [int] NOT NULL,
	[PatientDataId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.ZephyrBreathingWaveforms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ZephyrBRRRs]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZephyrBRRRs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[BR] [real] NULL,
	[RR] [real] NULL,
	[PatientDataId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.ZephyrBRRRs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ZephyrECGWaveforms]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZephyrECGWaveforms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Data] [int] NOT NULL,
	[PatientDataId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.ZephyrECGWaveforms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ZephyrEventDatas]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZephyrEventDatas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Time] [int] NOT NULL,
	[EventCode] [int] NOT NULL,
	[Type] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[EventId] [int] NOT NULL,
	[EventSpecificData] [nvarchar](max) NULL,
	[PatientDataId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.ZephyrEventDatas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ZephyrSummaryDatas]    Script Date: 4/17/2016 1:50:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ZephyrSummaryDatas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[HeartRate] [int] NOT NULL,
	[BreathingRate] [real] NULL,
	[SkinTemp] [real] NULL,
	[Posture] [int] NULL,
	[Activity] [real] NULL,
	[PeakAccel] [real] NULL,
	[BatteryVolts] [real] NULL,
	[BatteryLevel] [real] NULL,
	[BRAmplitude] [real] NULL,
	[BRNoise] [real] NULL,
	[BRConfidence] [int] NULL,
	[ECGAmplitude] [real] NULL,
	[ECGNoise] [real] NULL,
	[HRConfidence] [int] NULL,
	[HRV] [int] NULL,
	[SystemConfidence] [int] NULL,
	[GSR] [int] NULL,
	[ROGState] [int] NULL,
	[ROGTime] [int] NULL,
	[VerticalMin] [real] NULL,
	[VerticalPeak] [real] NULL,
	[LateralMin] [real] NULL,
	[LateralPeak] [real] NULL,
	[SagittalMin] [real] NULL,
	[SagittalPeak] [real] NULL,
	[DeviceTemp] [real] NULL,
	[StatusInfo] [int] NULL,
	[LinkQuality] [int] NULL,
	[RSSI] [int] NULL,
	[TxPower] [int] NULL,
	[CoreTemp] [real] NULL,
	[AuxADC1] [int] NULL,
	[AuxADC2] [int] NULL,
	[AuxADC3] [int] NULL,
	[PatientDataId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.ZephyrSummaryDatas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_PatientData_Id]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientData_Id] ON [dbo].[Activities]
(
	[PatientData_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_PatientDataId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientDataId] ON [dbo].[BasisPeakSummaryDatas]
(
	[PatientDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_ExperimentAdministrator_Id]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_ExperimentAdministrator_Id] ON [dbo].[Experiments]
(
	[ExperimentAdministrator_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MedicalDeviceId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_MedicalDeviceId] ON [dbo].[PatientDatas]
(
	[MedicalDeviceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Patient_Id]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Patient_Id] ON [dbo].[PatientDatas]
(
	[Patient_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Physician_Id]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_Physician_Id] ON [dbo].[Patients]
(
	[Physician_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_PatientDataId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientDataId] ON [dbo].[ZephyrAccelerometers]
(
	[PatientDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_PatientDataId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientDataId] ON [dbo].[ZephyrBreathingWaveforms]
(
	[PatientDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_PatientDataId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientDataId] ON [dbo].[ZephyrBRRRs]
(
	[PatientDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_PatientDataId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientDataId] ON [dbo].[ZephyrECGWaveforms]
(
	[PatientDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_PatientDataId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientDataId] ON [dbo].[ZephyrEventDatas]
(
	[PatientDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_PatientDataId]    Script Date: 4/17/2016 1:50:47 PM ******/
CREATE NONCLUSTERED INDEX [IX_PatientDataId] ON [dbo].[ZephyrSummaryDatas]
(
	[PatientDataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Activities] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [StartTime]
GO
ALTER TABLE [dbo].[Activities] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [EndTime]
GO
ALTER TABLE [dbo].[PatientDatas] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [FromDate]
GO
ALTER TABLE [dbo].[PatientDatas] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [ToDate]
GO
ALTER TABLE [dbo].[Patients] ADD  DEFAULT ('1900-01-01T00:00:00.000') FOR [Birthdate]
GO
ALTER TABLE [dbo].[Activities]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Activities_dbo.PatientDatas_PatientData_Id] FOREIGN KEY([PatientData_Id])
REFERENCES [dbo].[PatientDatas] ([Id])
GO
ALTER TABLE [dbo].[Activities] CHECK CONSTRAINT [FK_dbo.Activities_dbo.PatientDatas_PatientData_Id]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BasisPeakSummaryDatas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.BasisPeakSummaryDatas_dbo.PatientDatas_PatientDataId] FOREIGN KEY([PatientDataId])
REFERENCES [dbo].[PatientDatas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BasisPeakSummaryDatas] CHECK CONSTRAINT [FK_dbo.BasisPeakSummaryDatas_dbo.PatientDatas_PatientDataId]
GO
ALTER TABLE [dbo].[Experiments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Experiments_dbo.ExperimentAdministrators_ExperimentAdministrator_Id] FOREIGN KEY([ExperimentAdministrator_Id])
REFERENCES [dbo].[ExperimentAdministrators] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Experiments] CHECK CONSTRAINT [FK_dbo.Experiments_dbo.ExperimentAdministrators_ExperimentAdministrator_Id]
GO
ALTER TABLE [dbo].[PatientDatas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PatientDatas_dbo.MedicalDevices_MedicalDeviceId] FOREIGN KEY([MedicalDeviceId])
REFERENCES [dbo].[MedicalDevices] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientDatas] CHECK CONSTRAINT [FK_dbo.PatientDatas_dbo.MedicalDevices_MedicalDeviceId]
GO
ALTER TABLE [dbo].[PatientDatas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PatientDatas_dbo.Patients_Patient_Id] FOREIGN KEY([Patient_Id])
REFERENCES [dbo].[Patients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PatientDatas] CHECK CONSTRAINT [FK_dbo.PatientDatas_dbo.Patients_Patient_Id]
GO
ALTER TABLE [dbo].[Patients]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Patients_dbo.Physicians_Physician_Id] FOREIGN KEY([Physician_Id])
REFERENCES [dbo].[Physicians] ([Id])
GO
ALTER TABLE [dbo].[Patients] CHECK CONSTRAINT [FK_dbo.Patients_dbo.Physicians_Physician_Id]
GO
ALTER TABLE [dbo].[ZephyrAccelerometers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ZephyrAccelerometers_dbo.PatientDatas_PatientDataId] FOREIGN KEY([PatientDataId])
REFERENCES [dbo].[PatientDatas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ZephyrAccelerometers] CHECK CONSTRAINT [FK_dbo.ZephyrAccelerometers_dbo.PatientDatas_PatientDataId]
GO
ALTER TABLE [dbo].[ZephyrBreathingWaveforms]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ZephyrBreathingWaveforms_dbo.PatientDatas_PatientDataId] FOREIGN KEY([PatientDataId])
REFERENCES [dbo].[PatientDatas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ZephyrBreathingWaveforms] CHECK CONSTRAINT [FK_dbo.ZephyrBreathingWaveforms_dbo.PatientDatas_PatientDataId]
GO
ALTER TABLE [dbo].[ZephyrBRRRs]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ZephyrBRRRs_dbo.PatientDatas_PatientDataId] FOREIGN KEY([PatientDataId])
REFERENCES [dbo].[PatientDatas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ZephyrBRRRs] CHECK CONSTRAINT [FK_dbo.ZephyrBRRRs_dbo.PatientDatas_PatientDataId]
GO
ALTER TABLE [dbo].[ZephyrECGWaveforms]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ZephyrECGWaveforms_dbo.PatientDatas_PatientDataId] FOREIGN KEY([PatientDataId])
REFERENCES [dbo].[PatientDatas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ZephyrECGWaveforms] CHECK CONSTRAINT [FK_dbo.ZephyrECGWaveforms_dbo.PatientDatas_PatientDataId]
GO
ALTER TABLE [dbo].[ZephyrEventDatas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ZephyrEventDatas_dbo.PatientDatas_PatientDataId] FOREIGN KEY([PatientDataId])
REFERENCES [dbo].[PatientDatas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ZephyrEventDatas] CHECK CONSTRAINT [FK_dbo.ZephyrEventDatas_dbo.PatientDatas_PatientDataId]
GO
ALTER TABLE [dbo].[ZephyrSummaryDatas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ZephyrSummaryDatas_dbo.PatientDatas_PatientDataId] FOREIGN KEY([PatientDataId])
REFERENCES [dbo].[PatientDatas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ZephyrSummaryDatas] CHECK CONSTRAINT [FK_dbo.ZephyrSummaryDatas_dbo.PatientDatas_PatientDataId]
GO
USE [master]
GO
ALTER DATABASE [uahfitvault] SET  READ_WRITE 
GO
INSERT INTO [dbo].[AspNetUsers] ([Id], [Status], [PatientId], [PhysicianId], [ExperimentAdministratorId], [AccountRequestId], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'9193ee05-be6e-4a15-aca2-ac6f883af455', 1, 0, 0, 0, 1, N'uahfitvault@admin.com', 0, N'AP7vzkEaFbXCDylYXRNpwUu579mA3LIWqiuuYGLHYIJq63f8JwRTI5n9CwYN5begfw==', N'f98ddc1fd16f', NULL, 0, 0, NULL, 1, 0, N'fitadmin')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'3', N'Experiment Administrator')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'5', N'None')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Patient')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Physician')
INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'4', N'System Administrator')
SET IDENTITY_INSERT [dbo].[MedicalDevices] ON
INSERT INTO [dbo].[MedicalDevices] ([Id], [Name]) VALUES (1, N'Zephyr')
INSERT INTO [dbo].[MedicalDevices] ([Id], [Name]) VALUES (2, N'BasisPeak')
INSERT INTO [dbo].[MedicalDevices] ([Id], [Name]) VALUES (3, N'Microsoft Band')
SET IDENTITY_INSERT [dbo].[MedicalDevices] OFF