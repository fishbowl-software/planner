@echo off
cd ../src/Server/FishbowlSoftware.Planner.DbMigrator

:prompt
set "MigrationName="
set /p MigrationName="Enter Migration Name: "

if "%MigrationName%" == "" (
    echo Error: Migration name cannot be empty.
    goto prompt
)

echo Running migration for SQL Server...
dotnet ef migrations add %MigrationName% --project ../FishbowlSoftware.Planner.Migrations.SqlServer -- --provider SqlServer

echo Migrations completed.

echo Do you want to apply migrations (y/n):
set /p ApplyMigrationResult=

if /I "%ApplyMigrationResult%" == "y" (
	cd ../../../scripts
	call apply-migration.bat
)

pause
