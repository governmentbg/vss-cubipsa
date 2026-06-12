
SET NOCOUNT ON

ALTER TABLE [dbo].[Acts]
ADD 
	[CreateDate] [datetime2](7) NULL,
	[ModifyDate] [datetime2](7) NULL
GO
