CREATE TABLE [dbo].[Logs](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[ActionLogTypeId] [int] NOT NULL,
	[DatetimeOfAction] [datetime] NOT NULL,
	[CourtId] [int] NOT NULL,
	[CaseNumber] [int] NULL,
	[ActKindId] [int] NULL,
	[UID] [nvarchar](40) NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY ([LogId])
)
GO