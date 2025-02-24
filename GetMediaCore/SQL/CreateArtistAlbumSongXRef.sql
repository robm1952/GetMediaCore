USE [Media]
GO

/****** Object:  Table [dbo].[ArtistAlbumSongXref]    Script Date: 12/12/2024 19:21:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ArtistAlbumSongXref](
	[RefId] [int] IDENTITY(1,1) NOT NULL,
	[ArtistId] [int] NOT NULL,
	[AlbumId] [int] NOT NULL,
	[SongId] [int] NOT NULL,
 CONSTRAINT [PK_AlbumSongXref] PRIMARY KEY CLUSTERED 
(
	[RefId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

