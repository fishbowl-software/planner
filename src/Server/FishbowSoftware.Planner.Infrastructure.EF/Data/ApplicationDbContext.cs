using FishbowSoftware.Planner.Domain.Entities;
using FishbowSoftware.Planner.Infrastructure.Helpers;
using FishbowSoftware.Planner.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FishbowSoftware.Planner.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        private readonly ApplicationDbContextOptions _dbContextOptions;
        private readonly AuditingEntitiesInterceptor? _auditingEntitiesInterceptor;
        private readonly DispatchDomainEventsInterceptor? _dispatchDomainEventsInterceptor;
        private readonly IServiceProvider? _serviceProvider;

        public ApplicationDbContext(
            ApplicationDbContextOptions dbContextOptions,
            AuditingEntitiesInterceptor? auditingEntitiesInterceptor = default,
            DispatchDomainEventsInterceptor? dispatchDomainEventsInterceptor = default,
            IServiceProvider? serviceProvider = default)
        {
            _auditingEntitiesInterceptor = auditingEntitiesInterceptor;
            _dispatchDomainEventsInterceptor = dispatchDomainEventsInterceptor;
            _dbContextOptions = dbContextOptions;
            _serviceProvider = serviceProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var availableDatabaseProviders = new List<string>();

            if (_dispatchDomainEventsInterceptor is not null)
            {
                options.AddInterceptors(_dispatchDomainEventsInterceptor);
            }
            if (_auditingEntitiesInterceptor is not null)
            {
                options.AddInterceptors(_auditingEntitiesInterceptor);
            }

            if (string.IsNullOrEmpty(_dbContextOptions.DatabaseProvider))
            {
                throw new ArgumentException("The database provider is not specified");
            }
            if (string.IsNullOrEmpty(_dbContextOptions.ConnectionString))
            {
                throw new ArgumentException("The connection string is not specified");
            }

            availableDatabaseProviders.Add("SqlServer");

            if (_dbContextOptions.DatabaseProvider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
            {
                DbContextHelpers.ConfigureSqlServer(_dbContextOptions.ConnectionString, options);
            }




            if (!_dbContextOptions.DatabaseProvider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase) &&
                !_dbContextOptions.DatabaseProvider.Equals("Sqlite", StringComparison.OrdinalIgnoreCase) &&
                !_dbContextOptions.DatabaseProvider.Equals("PostgreSql", StringComparison.OrdinalIgnoreCase) &&
                !_dbContextOptions.DatabaseProvider.Equals("MySql", StringComparison.OrdinalIgnoreCase))
            {
                throw new ArgumentException("The database provider is invalid. The available database providers are: " +
                                        string.Join(", ", availableDatabaseProviders));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public TService? GetService<TService>()
        {
            if (_serviceProvider is null)
            {
                return default;
            }

            return (TService?)_serviceProvider.GetService(typeof(TService));
        }

        public TService GetRequiredService<TService>()
        {
            var service = GetService<TService>();

            if (service is null)
            {
                throw new InvalidOperationException($"The required service {typeof(TService).Name} is not registered");
            }

            return service;
        }
    }
}
