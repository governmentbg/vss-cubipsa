CREATE TABLE [dbo].[ActionLogTypes](
	[ActionLogTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ActionLogTypes] PRIMARY KEY ([ActionLogTypeId])
)
GO