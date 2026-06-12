USE [$(dbName)]
GO

---------------------------------------------------------------
-- Tables
---------------------------------------------------------------

:r $(rootPath)\"Tables\ActionLogTypes.sql"
:r $(rootPath)\"Tables\ActKinds.sql"
:r $(rootPath)\"Tables\Acts.sql"
:r $(rootPath)\"Tables\ActDocuments.sql"
:r $(rootPath)\"Tables\MotiveDocuments.sql"
:r $(rootPath)\"Tables\AppealKinds.sql"
:r $(rootPath)\"Tables\CaseKinds.sql"
:r $(rootPath)\"Tables\ConnectedActs.sql"
:r $(rootPath)\"Tables\ConnectedCases.sql"
:r $(rootPath)\"Tables\ConnectedKinds.sql"
:r $(rootPath)\"Tables\ConnectedTypes.sql"
:r $(rootPath)\"Tables\Courts.sql"
:r $(rootPath)\"Tables\HigherCourts.sql"
:r $(rootPath)\"Tables\IndocKinds.sql"
:r $(rootPath)\"Tables\Involvements.sql"
:r $(rootPath)\"Tables\Links.sql"
:r $(rootPath)\"Tables\Logs.sql"
:r $(rootPath)\"Tables\Permissions.sql"
:r $(rootPath)\"Tables\ResultsOfAppeals.sql"
:r $(rootPath)\"Tables\Roles.sql"
:r $(rootPath)\"Tables\RolesPermissions.sql"
:r $(rootPath)\"Tables\SendToDocumentKinds.sql"
:r $(rootPath)\"Tables\Statuses.sql"
:r $(rootPath)\"Tables\Users.sql"
:r $(rootPath)\"Tables\UsersRoles.sql"

:r $(rootPath)\"Tables\SystemLogs.sql"
:r $(rootPath)\"Tables\Messages.sql"

---------------------------------------------------------------
-- Constraints
---------------------------------------------------------------

:r ".\Constraints\Constraints.sql"

---------------------------------------------------------------
-- Programming (sp)
---------------------------------------------------------------

:r ".\Programmability\sp\spMergeConnectedActs.sql"

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

-- :r ".\Diagram\Main.sql"

---------------------------------------------------------------
--Inserts
---------------------------------------------------------------
:r ".\Inserts\ActionLogTypes.sql"
:r ".\Inserts\ActKinds.sql"
:r ".\Inserts\AppealKinds.sql"
:r ".\Inserts\CaseKinds.sql"
:r ".\Inserts\ConnectedKinds.sql"
:r ".\Inserts\ConnectedTypes.sql"
:r ".\Inserts\Courts.sql"
:r ".\Inserts\IndocKinds.sql"
:r ".\Inserts\Involvements.sql"
:r ".\Inserts\ResultsOfAppeals.sql"
:r ".\Inserts\Roles.sql"
:r ".\Inserts\SendToDocumentKinds.sql"
:r ".\Inserts\Statuses.sql"
:r ".\Inserts\Users.sql"
:r ".\Inserts\UsersRoles.sql"

---------------------------------------------------------------
--Updates
---------------------------------------------------------------
:r ".\Updates\2018-03-13-add-courts-eclicodes.sql"
:r ".\Updates\2018-03-13-add-casekinds-eclicodes.sql"
:r ".\Updates\2018-03-13-add-acts-eclicode-column.sql"
:r ".\Updates\2018-06-08-deleted-acts.sql"