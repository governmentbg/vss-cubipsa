CREATE TABLE [dbo].[AppealKinds](
	[AppealKindId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_AppealKinds] PRIMARY KEY ([AppealKindId]) 
)
GO