USE [$(dbName)]
GO

---------------------------------------------------------------
-- Tables
---------------------------------------------------------------

:r $(rootPath)\"Tables\Routes.sql"
:r $(rootPath)\"Tables\Indexes.sql"
:r $(rootPath)\"Tables\Sitemaps.sql"
:r $(rootPath)\"Tables\SystemVariables.sql"

---------------------------------------------------------------
-- Constraints
---------------------------------------------------------------

:r ".\Constraints\Constraints.sql"

---------------------------------------------------------------
-- Inserts
---------------------------------------------------------------

:r ".\Insert\SystemVariables.sql"