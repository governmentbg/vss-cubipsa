CREATE TABLE [dbo].[ConnectedActs](
	[ActId] [int] NOT NULL,
	[ConnectedActId] [int] NOT NULL,
	CONSTRAINT [PK_ConnectedActs] PRIMARY KEY ([ActId], [ConnectedActId])
)
GO