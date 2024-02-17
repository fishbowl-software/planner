using Microsoft.EntityFrameworkCore;

namespace FishbowSoftware.Planner.Infrastructure.Helpers
{
    internal static class DbContextHelpers
    {
        public static void ConfigureSqlServer(string connectionString, DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionString, o =>
            {
                o.MigrationsAssembly("FishbowSoftware.Planner.Migrations.SqlServer");
                o.EnableRetryOnFailure(8, TimeSpan.FromSeconds(15), null);
            });
        }

    }
}
