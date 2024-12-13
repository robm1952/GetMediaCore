USE [Media]
GO

/****** Object:  Table [dbo].[Songs]    Script Date: 12/12/2024 19:23:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Songs](
	[songID] [int] IDENTITY(1,1) NOT NULL,
	[songAlbumId] [int] NOT NULL,
	[songTitle] [nvarchar](250) NOT NULL,
	[songDuration] [bigint] NOT NULL,
	[songTrackNumber] [int] NOT NULL,
 CONSTRAINT [PK_Songs] PRIMARY KEY CLUSTERED 
(
	[songID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

