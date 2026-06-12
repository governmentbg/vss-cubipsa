CREATE PROCEDURE [dbo].[spMergeConnectedActs]
AS 
BEGIN
    MERGE ConnectedActs AS CA
	USING 
		(SELECT a.ActId AS ActId, acon.ActId AS ConnectedActId
		FROM Acts a 
			INNER JOIN ConnectedCases cc ON cc.ActId = a.ActId 
			INNER JOIN  Acts acon ON 
			acon.CaseNumber = cc.CaseNumber 
			AND acon.CaseYear = cc.Year
			AND (
			(cc.ConnectedKindId IN (4001) AND acon.CaseKindId IN (2001,2002,2003,2004,2005,2006,2007,2008,2009,2031)) 
			OR (cc.ConnectedKindId IN (4002) AND acon.CaseKindId IN (2011, 2012, 2013, 2023, 2024, 2027, 2029)) 
			OR (cc.ConnectedKindId IN (4003) AND acon.CaseKindId IN (2014, 2015, 2016, 2017, 2025, 2026, 2028, 2030))
			OR (cc.ConnectedKindId IN (4004) AND acon.CaseKindId IN (2018, 2019, 2020))
			OR (cc.ConnectedKindId IN (4005) AND acon.CaseKindId IN (2021))
			OR (cc.ConnectedKindId IN (4006) AND acon.CaseKindId IN (2022))
			)
			INNER JOIN HigherCourts aconhc ON acon.HigherCourtId = aconhc.HigherCourtId
		WHERE 
		a.CourtId = aconhc.CourtId) AS A
	ON (CA.ActId = A.ActId)
	WHEN NOT MATCHED THEN
	  INSERT (ActId, ConnectedActId) VALUES (A.ActId, A.ConnectedActId);
RETURN
END
GO