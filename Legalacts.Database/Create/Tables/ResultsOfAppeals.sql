CREATE TABLE [dbo].[ResultsOfAppeals](
	[ResultsOfAppealId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](MAX) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_ResultsOfAppeals] PRIMARY KEY ([ResultsOfAppealId])
)
GO