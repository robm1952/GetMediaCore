USE [Media]
GO

/****** Object:  Table [dbo].[SongFiles]    Script Date: 12/12/2024 19:23:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SongFiles](
	[SongFileId] [int] IDENTITY(1,1) NOT NULL,
	[SongFileSongId] [int] NOT NULL,
	[SongFileFullyQualifiedName] [nvarchar](300) NULL,
	[SongFileSize] [bigint] NULL,
	[SongFileType] [bit] NULL,
 CONSTRAINT [PK_SongFiles] PRIMARY KEY CLUSTERED 
(
	[SongFileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

