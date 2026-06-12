
SET NOCOUNT ON

ALTER TABLE [dbo].[CaseKinds]
ADD 
[EcliCode] nvarchar(2) NULL,
[Abbreviation] nvarchar(50) NULL
GO

UPDATE CaseKinds SET EcliCode = N'07', Abbreviation = N'адм. д.' WHERE CaseKindId = 2018
UPDATE CaseKinds SET EcliCode = N'02', Abbreviation = N'а. н. д.' WHERE CaseKindId = 2003
UPDATE CaseKinds SET EcliCode = N'01', Abbreviation = N'б. д.' WHERE CaseKindId = 2022
UPDATE CaseKinds SET EcliCode = N'06', Abbreviation = N'в. а. н. д.' WHERE CaseKindId = 2009
UPDATE CaseKinds SET EcliCode = N'06', Abbreviation = N'в. н. о. х. д.' WHERE CaseKindId = 2005
UPDATE CaseKinds SET EcliCode = N'06', Abbreviation = N'в. н. ч. х. д.' WHERE CaseKindId = 2006
UPDATE CaseKinds SET EcliCode = N'06', Abbreviation = N'в. ч. н. д.' WHERE CaseKindId = 2008
UPDATE CaseKinds SET EcliCode = N'05', Abbreviation = N'в. гр. д.' WHERE CaseKindId = 2012
UPDATE CaseKinds SET EcliCode = N'09', Abbreviation = N'в. т. д.' WHERE CaseKindId = 2016
UPDATE CaseKinds SET EcliCode = N'05', Abbreviation = N'в. ч. гр. д.' WHERE CaseKindId = 2013
UPDATE CaseKinds SET EcliCode = N'09', Abbreviation = N'в. ч. т. д.' WHERE CaseKindId = 2017
UPDATE CaseKinds SET EcliCode = N'01', Abbreviation = N'гр. д.' WHERE CaseKindId = 2010
UPDATE CaseKinds SET EcliCode = N'01', Abbreviation = N'гр. д. н.' WHERE CaseKindId = 2027
UPDATE CaseKinds SET EcliCode = N'05', Abbreviation = N'в. гр. д. н.' WHERE CaseKindId = 2029
UPDATE CaseKinds SET EcliCode = N'06', Abbreviation = N'к. а. н. д.' WHERE CaseKindId = 2007
UPDATE CaseKinds SET EcliCode = N'07', Abbreviation = N'к. а. д.' WHERE CaseKindId = 2020
UPDATE CaseKinds SET EcliCode = N'05', Abbreviation = N'к. гр. д.' WHERE CaseKindId = 2023
UPDATE CaseKinds SET EcliCode = N'06', Abbreviation = N'к. н. д.' WHERE CaseKindId = 2031
UPDATE CaseKinds SET EcliCode = N'09', Abbreviation = N'к. т. д.' WHERE CaseKindId = 2025
UPDATE CaseKinds SET EcliCode = N'07', Abbreviation = N'к. ч. а. д.' WHERE CaseKindId = 2034
UPDATE CaseKinds SET EcliCode = N'05', Abbreviation = N'к. ч. гр. д.' WHERE CaseKindId = 2024
UPDATE CaseKinds SET EcliCode = N'06', Abbreviation = N'к. ч. н. д.' WHERE CaseKindId = 2035
UPDATE CaseKinds SET EcliCode = N'09', Abbreviation = N'к. ч. т. д.' WHERE CaseKindId = 2026
UPDATE CaseKinds SET EcliCode = N'06', Abbreviation = N'к. ч. а. н. д.' WHERE CaseKindId = 2033
UPDATE CaseKinds SET EcliCode = N'02', Abbreviation = N'н. о. х. д.' WHERE CaseKindId = 2001
UPDATE CaseKinds SET EcliCode = N'02', Abbreviation = N'н. ч. х. д.' WHERE CaseKindId = 2002
UPDATE CaseKinds SET EcliCode = N'09', Abbreviation = N'т. д.' WHERE CaseKindId = 2014
UPDATE CaseKinds SET EcliCode = N'09', Abbreviation = N'т. д. н.' WHERE CaseKindId = 2028
UPDATE CaseKinds SET EcliCode = N'09', Abbreviation = N'в. т. д. н.' WHERE CaseKindId = 2030
UPDATE CaseKinds SET EcliCode = N'08', Abbreviation = N'ф. д.' WHERE CaseKindId = 2021
UPDATE CaseKinds SET EcliCode = N'02', Abbreviation = N'ч. а. н. д.' WHERE CaseKindId = 2032
UPDATE CaseKinds SET EcliCode = N'07', Abbreviation = N'ч. а. д.' WHERE CaseKindId = 2019
UPDATE CaseKinds SET EcliCode = N'01', Abbreviation = N'ч. гр. д.' WHERE CaseKindId = 2011
UPDATE CaseKinds SET EcliCode = N'09', Abbreviation = N'ч. т. д.' WHERE CaseKindId = 2015
UPDATE CaseKinds SET EcliCode = N'02', Abbreviation = N'ч. н. д.' WHERE CaseKindId = 2004
GO

ALTER TABLE [dbo].[CaseKinds]
ALTER COLUMN [EcliCode] nvarchar(2) NOT NULL
GO

ALTER TABLE [dbo].[CaseKinds]
ALTER COLUMN [Abbreviation] nvarchar(50) NOT NULL
GO

SET NOCOUNT OFF