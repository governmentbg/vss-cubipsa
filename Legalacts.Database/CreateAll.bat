for /f %%i in ('chdir') do set currentDir=%%i
@sqlcmd -S. -v dbName="Legalacts" -i"CreateAll.sql"