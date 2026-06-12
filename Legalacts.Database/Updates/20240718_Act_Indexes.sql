
-- Available INDEXES
-----------------------
-- IX_ActNumber
-- IX_CaseNumer
-- IX_UID
-- IX_EcliCode
-- IX_PreviousEcliCode

CREATE INDEX [IX_Acts_CaseNumber_CaseYear] ON [dbo].[Acts]([CaseNumber], [CaseYear])
GO

CREATE INDEX [IX_Acts_ActNumber_ActYear] ON [dbo].[Acts]([ActNumber], [ActYear])
GO

CREATE INDEX [IX_Acts_StartDate] ON [dbo].[Acts]([StartDate])
GO

CREATE INDEX [IX_Acts_LegalDate] ON [dbo].[Acts]([LegalDate])
GO



CREATE INDEX [IX_Acts_ActKindId] ON [dbo].[Acts]([ActKindId])
GO

CREATE INDEX [IX_Acts_CaseKindId] ON [dbo].[Acts]([CaseKindId])
GO

CREATE INDEX [IX_Acts_CourtId] ON [dbo].[Acts]([CourtId])
GO

CREATE INDEX [IX_Acts_ActDocumentId] ON [dbo].[Acts]([ActDocumentId])
GO

CREATE INDEX [IX_Acts_MotiveDocumentId] ON [dbo].[Acts]([MotiveDocumentId])
GO

CREATE INDEX [IX_Acts_HigherCourtId] ON [dbo].[Acts]([HigherCourtId])
GO

CREATE INDEX [IX_Acts_StatusId] ON [dbo].[Acts]([StatusId])
GO