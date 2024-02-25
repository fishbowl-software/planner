using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FishbowlSoftware.Planner.DbMigrator.Data
{
    public partial class SeedData : BackgroundService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ILogger<SeedData> _logger;
        private readonly IConfiguration _configuration;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IUnitOfWork _uow;

        public SeedData(
            IHostApplicationLifetime appLifetime,
            IConfiguration configuration,
            ILogger<SeedData> logger,
            IServiceScopeFactory serviceScopeFactory,
            IUnitOfWork uow)
        {
            _appLifetime = appLifetime;
            _configuration = configuration;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _uow = uow;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            await SeedDatabaseAsync(environment ?? "Development");
            _appLifetime.StopApplication();
        }

        private async Task SeedDatabaseAsync(string environment = "Development")
        {
            var scope = _serviceScopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            _logger.LogInformation("Initializing the database...");
            await MigrateDatabaseAsync(applicationDbContext);
            _logger.LogInformation("Successfully initialized the database");

            switch (environment)
            {
                case "Development":
                    await SeedDatabaseAsync_Development();
                    break;
                case "Production":
                    await SeedDatabaseAsync_Production();
                    break;
            }
        }

        private static async Task MigrateDatabaseAsync(DbContext databaseContext)
        {
            await databaseContext.Database.MigrateAsync();
        }
    }

    internal record UserData(string Email, string Password);
}
