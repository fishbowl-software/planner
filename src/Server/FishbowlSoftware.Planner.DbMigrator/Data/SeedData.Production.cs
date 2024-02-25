namespace FishbowlSoftware.Planner.DbMigrator.Data
{
    public partial class SeedData
    {
        private Task SeedDatabaseAsync_Production()
        {
            _logger.LogInformation("[Production] Seeding the database...");
            _logger.LogInformation("Successfully seeded the database");
            return Task.CompletedTask;
        }
    }
}
