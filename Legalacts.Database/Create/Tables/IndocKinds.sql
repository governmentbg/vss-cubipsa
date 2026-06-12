CREATE TABLE [dbo].[IndocKinds](
	[IndocKindId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_IndocKinds] PRIMARY KEY ([IndocKindId])
)
GO