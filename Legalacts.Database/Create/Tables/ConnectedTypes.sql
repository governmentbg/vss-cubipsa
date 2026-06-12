CREATE TABLE [dbo].[ConnectedTypes](
	[ConnectedTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ConnectTypes] PRIMARY KEY ([ConnectedTypeId])
)
GO