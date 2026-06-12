
-- update CaseKind
update CaseKinds set name = 'АНД' where CaseKindId = 2003
update CaseKinds set name = 'КАНД' where CaseKindId = 2007
update CaseKinds set name = 'ВАНД' where CaseKindId = 2009

SET NOCOUNT ON
SET IDENTITY_INSERT [dbo].[CaseKinds] ON

insert into [CaseKinds] ([CaseKindId], Name, IsActive) values (2035, N'Касационно частно наказателно дело', 1)

SET NOCOUNT OFF
SET IDENTITY_INSERT [dbo].[CaseKinds] OFF
GO

-- update ActKinds
SET NOCOUNT ON
SET IDENTITY_INSERT [dbo].[ActKinds] ON

insert into ActKinds (ActKindId, Name, IsActive) values (5008, 'Становище', 1)
insert into ActKinds (ActKindId, Name, IsActive) values (5009, 'Постановление', 1)

SET NOCOUNT OFF
SET IDENTITY_INSERT [dbo].[ActKinds] OFF
GO

-- update IndocKinds
SET NOCOUNT ON
SET IDENTITY_INSERT [dbo].[IndocKinds] ON

insert into [IndocKinds] (IndocKindId, Name, IsActive) values (8062, 'Предложение', 1)
insert into [IndocKinds] (IndocKindId, Name, IsActive) values (8063, 'Установителен иск', 1)

SET NOCOUNT OFF
SET IDENTITY_INSERT [dbo].[IndocKinds] OFF
GO