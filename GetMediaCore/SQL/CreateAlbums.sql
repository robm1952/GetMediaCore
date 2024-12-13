USE [Media]
GO

/****** Object:  Table [dbo].[Albums]    Script Date: 12/12/2024 19:20:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Albums](
	[albumId] [int] IDENTITY(1,1) NOT NULL,
	[albumArtistId] [int] NOT NULL,
	[albumTitle] [nvarchar](250) NULL,
	[albumGenre] [int] NULL,
	[albumYear] [int] NULL,
	[albumDisc] [int] NULL,
 CONSTRAINT [PK_Album] PRIMARY KEY CLUSTERED 
(
	[albumId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

