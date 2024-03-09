using Microsoft.EntityFrameworkCore;

namespace FishbowlSoftware.Planner.Infrastructure.Helpers;

internal static class DbContextHelpers
{
    public static void ConfigureSqlServer(string connectionString, DbContextOptionsBuilder options)
    {
        options.UseSqlServer(connectionString, o =>
        {
            // TODO: move migrations to the Migrations.SqlServer project upon production deployment and uncomment this line
            // o.MigrationsAssembly("FishbowlSoftware.Planner.Migrations.SqlServer"); 
            o.EnableRetryOnFailure(8, TimeSpan.FromSeconds(15), null);
        });
    }
}
