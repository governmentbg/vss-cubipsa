
CREATE TABLE [dbo].[Sitemaps](
	[SitemapId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[XML] [varbinary](max) NOT NULL,
	[IndexId] [int] NOT NULL
 CONSTRAINT [PK_Sitemaps] PRIMARY KEY ([SitemapId])
)
GO