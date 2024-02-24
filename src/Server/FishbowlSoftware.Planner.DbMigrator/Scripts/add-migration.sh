#!/bin/bash

echo "Enter Migration Name: "
read -r MigrationName

if [ -z "$MigrationName" ]
then
    echo "Error: Migration name cannot be empty."
    exit 1
fi

echo "Running migration for SQL Server..."
dotnet ef migrations add "$MigrationName" --project ../FishbowlSoftware.Planner.Migrations.SqlServer -- --provider SqlServer




echo "Migrations completed."

echo "Do you want to apply migrations (y/n): "
read -r ApplyMigrationResult

if [ "$ApplyMigrationResult" = "y" ]
then
    ./apply-migration.sh
fi
