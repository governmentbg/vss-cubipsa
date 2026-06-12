CREATE TABLE [dbo].[HigherCourts](
	[HigherCourtId] [int] IDENTITY(1,1) NOT NULL,
	[CourtId] [int] NOT NULL,
	[OutputNumber] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[SendToDocumentKindId] [int] NOT NULL,
	[DateOfDispatch] [date] NULL,
	CONSTRAINT [PK_HigherCourts] PRIMARY KEY ([HigherCourtId])
)
GO