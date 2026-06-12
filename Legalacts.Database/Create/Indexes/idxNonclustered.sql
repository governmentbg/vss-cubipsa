USE [Legalacts]
GO

CREATE NONCLUSTERED INDEX [IX_ActNumber]
ON [dbo].[Acts] ([ActNumber]);
GO

CREATE NONCLUSTERED INDEX [IX_CaseNumber]
ON [dbo].[Acts] ([CaseNumber]);
GO

CREATE NONCLUSTERED INDEX [IX_UID]
ON [dbo].[Acts] ([UID]);
GO