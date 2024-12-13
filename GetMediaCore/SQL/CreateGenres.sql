USE [Media]
GO

/****** Object:  Table [dbo].[Genres]    Script Date: 12/12/2024 19:22:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Genres](
	[genreId] [int] IDENTITY(1,1) NOT NULL,
	[genreName] [varchar](20) NULL
) ON [PRIMARY]
GO

