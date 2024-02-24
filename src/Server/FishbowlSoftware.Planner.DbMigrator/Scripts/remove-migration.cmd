@echo off

echo Do you want to remove the latest migration (y/n):
set /p UserPromptResult=

if /I "%UserPromptResult%" == "y" (

    echo Removing the latest migration from SQL Server...
    dotnet ef migrations remove --project ../FishbowlSoftware.Planner.Migrations.SqlServer -- --provider SqlServer
    
    
    
)

echo Removed the latest migration
pause
