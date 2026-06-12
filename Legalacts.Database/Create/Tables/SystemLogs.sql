CREATE TABLE [dbo].[SystemLogs](
	[SystemLogId] [int] IDENTITY(1,1) NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[LogDate] [datetime] NULL,
	[IP] [nvarchar](50) NULL,
	[RawUrl] [nvarchar](500) NULL,
	[Form] [nvarchar](500) NULL,
	[UserAgent] [nvarchar](200) NULL,
	[SessionId] [nvarchar](50) NULL,
	[RequestId] [uniqueidentifier] NULL,
	[Message] [nvarchar](max) NULL,
 CONSTRAINT [PK_SystemLogs] PRIMARY KEY ([SystemLogId])
 )
 GO

