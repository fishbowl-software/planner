@echo off
cd ../src/Server/FishbowlSoftware.Planner.DbMigrator

echo Applying migrations...
dotnet run

echo Successfully applied migrations.
pause
