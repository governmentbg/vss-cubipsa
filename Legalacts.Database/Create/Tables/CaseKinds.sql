CREATE TABLE [dbo].[CaseKinds](
	[CaseKindId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_CaseKinds] PRIMARY KEY ([CaseKindId])
)
GO