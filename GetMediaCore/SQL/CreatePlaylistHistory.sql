USE [Media]
GO

/****** Object:  Table [dbo].[PlaylistHistory]    Script Date: 12/12/2024 19:22:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PlaylistHistory](
	[M3UID] [int] IDENTITY(1,1) NOT NULL,
	[Artist] [nvarchar](175) NULL,
	[Album] [nvarchar](150) NULL,
	[Year] [int] NULL,
	[Genre] [nvarchar](25) NULL,
	[SongTitle] [nvarchar](250) NULL,
	[SongPath] [nvarchar](max) NULL,
	[M3UName] [nvarchar](50) NULL,
	[DateCreated] [date] NULL,
 CONSTRAINT [PK_PlaylistHistory] PRIMARY KEY CLUSTERED 
(
	[M3UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

