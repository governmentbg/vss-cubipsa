CREATE TABLE [dbo].[ConnectedKinds](
	[ConnectedKindId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
	CONSTRAINT [PK_ConnectKinds] PRIMARY KEY ([ConnectedKindId])
)
GO