namespace FishbowlSoftware.Planner.DbMigrator.Data
{
    public partial class SeedData
    {
        private Task SeedDatabaseAsync_Development()
        {
            _logger.LogInformation("[Development] Seeding the database...");
            _logger.LogInformation("Successfully seeded the database");
            return Task.CompletedTask;
        }
    }
}
