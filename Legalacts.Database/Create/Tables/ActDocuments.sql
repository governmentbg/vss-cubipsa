CREATE TABLE [dbo].[ActDocuments](
	[ActDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [varbinary](max) NOT NULL,
	[MimeType] [nvarchar](50) NOT NULL,
	[Extension] [nvarchar](10) NOT NULL,
CONSTRAINT [PK_ActDocuments] PRIMARY KEY ([ActDocumentId])
) 
GO
