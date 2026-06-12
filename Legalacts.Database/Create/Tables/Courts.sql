CREATE TABLE [dbo].[Courts](
	[CourtId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Courts] PRIMARY KEY ([CourtId])
)
GO