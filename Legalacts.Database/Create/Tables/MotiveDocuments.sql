CREATE TABLE [dbo].[MotiveDocuments](
	[MotiveDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [varbinary](max) NOT NULL,
	[MimeType] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MotiveDocuments] PRIMARY KEY ([MotiveDocumentId])
) 
GO
