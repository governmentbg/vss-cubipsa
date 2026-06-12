SET QUOTED_IDENTIFIER ON
GO

SET NOCOUNT ON

PRINT '------ Creating Legalacts'
:setvar rootPath ".\Create"
:r $(rootPath)"\CreateDB.sql"
:r $(rootPath)"\Create.sql"

SET NOCOUNT OFF