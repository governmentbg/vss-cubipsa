CREATE TABLE [dbo].[SendToDocumentKinds](
	[SendToDocumentKindId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_SendToKinds] PRIMARY KEY ([SendToDocumentKindId])
)
GO