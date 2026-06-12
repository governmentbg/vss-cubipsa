
SET NOCOUNT ON

ALTER TABLE [dbo].[Acts]
ADD 
	[EcliCode] nvarchar(37) NULL,
	[PreviousEcliCode] nvarchar(37) NULL
GO

CREATE INDEX IX_EcliCode
ON Acts(EcliCode)