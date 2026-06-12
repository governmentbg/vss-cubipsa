CREATE TABLE [dbo].[Acts](
	[ActId] [int] IDENTITY(1,1) NOT NULL,
	[ActNumber] [int] NULL,
	[CaseNumber] [int] NOT NULL,
	[Judge] [nvarchar](200) NULL,
	[ActKindId] [int] NOT NULL,
	[CaseKindId] [int] NOT NULL,
	[CaseYear] [int] NOT NULL,
	[ActYear] [int] NULL,
	[CourtId] [int] NOT NULL,
	[StartDate] [date] NULL,
	[ActDocumentId] [int] NULL,
	[MotiveDocumentId] [int] NULL,
	[MotiveDate] [date] NULL,
	[LegalDate] [date] NULL,
	[HigherCourtId] [int] NULL,
	[StatusId] [int] NOT NULL,
	[ResultOfAppeal] [nvarchar](10) NULL,
	[UID] [nvarchar](40) NULL,
	[CreateDate] [datetime2](7) NULL,
	[ModifyDate] [datetime2](7) NULL
 CONSTRAINT [PK_Acts] PRIMARY KEY ([ActId])
)
GO