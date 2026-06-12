CREATE TABLE [dbo].[ConnectedCases](
	[ConnectedCaseId] [int] IDENTITY(1,1) NOT NULL,
	[ConnectedTypeId] [int] NOT NULL,
	[ConnectedKindId] [int] NOT NULL,
	[ActId] [int] NOT NULL,
	[CourtId] [int] NOT NULL,
	[AppealActKindId] [int] NOT NULL,
	[CaseNumber] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[NumberOfAppealAct] [int] NULL,
	[DateOfAppealAct] [date] NULL,
	CONSTRAINT [PK_ConnectedCases] PRIMARY KEY ([ConnectedCaseId])
)
GO