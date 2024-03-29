# Fishbowl Software Planner
## How to run the project?
1. **Update Database Connection Strings:**
  - Navigate to the [appsettings.json](./src/Server/FishbowlSoftware.Planner.API/appsettings.json) file.
  - Modify the `DatabaseConfig.ConnectionString` section with your database connection string.

2. **Apply Database Migration:**  
- Run the [apply-migration](./src/Server/FishbowlSoftware.Planner.DbMigrator/Scripts/apply-migration.cmd) CMD script file to apply the database migration.

3. **Run the API Application:**
- Execute the [run-api](./scripts/run-api.cmd) CMD script file to start the ASP.NET Core Web API application.

4. **Run the Identity Server Application:**
- Execute the [run-identity](./scripts/run-api.cmd) CMD script file to start the ASP.NET Core Identity Server application.

5. **Run the Client Application:**
- Execute the [run-client](./scripts/run-client.cmd) CMD script file to start the Angular client application.

## Associated localhost addresses for each application:
- https://localhost:7000 - Web API application
- https://localhost:7001 - Identity Server application
- https://localhost:7002 - Angular SPA
