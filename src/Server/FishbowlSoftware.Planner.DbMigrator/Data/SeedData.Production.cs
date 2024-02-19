namespace FishbowlSoftware.Planner.DbMigrator.Data
{
    public partial class SeedData
    {
        private async Task SeedDatabaseAsync_Production()
        {
            _logger.LogInformation("[Production] Seeding the database...");
            await AddBuiltInRolesAsync();
            await AddAdminAsync();
            _logger.LogInformation("Successfully seeded the database");
        }
    }
}
