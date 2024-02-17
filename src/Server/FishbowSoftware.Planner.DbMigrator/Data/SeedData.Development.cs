namespace FishbowSoftware.Planner.DbMigrator.Data
{
    public partial class SeedData
    {
        private async Task SeedDatabaseAsync_Development()
        {
            _logger.LogInformation("[Development] Seeding the database...");
            await AddBuiltInRolesAsync();
            await AddAdminAsync();
            _logger.LogInformation("Successfully seeded the database");
        }
    }
}
