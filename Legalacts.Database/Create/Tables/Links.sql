CREATE TABLE [dbo].[Links](
	[LinkId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Links] PRIMARY KEY ([LinkId])
)
GO