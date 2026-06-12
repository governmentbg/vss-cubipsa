
CREATE TABLE [dbo].[Indexes](
	[IndexId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[XML] [varbinary](max) NOT NULL,
	[RouteId] [int] NOT NULL
 CONSTRAINT [PK_Indexes] PRIMARY KEY ([IndexId])
)
GO