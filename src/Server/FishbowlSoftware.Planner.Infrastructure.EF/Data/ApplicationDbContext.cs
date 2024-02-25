using FishbowlSoftware.Planner.Infrastructure.Data.EntityConfigurations;
using FishbowlSoftware.Planner.Infrastructure.Helpers;
using FishbowlSoftware.Planner.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace FishbowlSoftware.Planner.Infrastructure.Data;

public class ApplicationDbContext : DbContext
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
        builder.ApplyConfiguration(new ApplicationEntityMap());
        builder.ApplyConfiguration(new ApplicationObjectEntityMap());
        builder.ApplyConfiguration(new ClientEntityMap());
        builder.ApplyConfiguration(new FlowEntityMap());
        builder.ApplyConfiguration(new ProjectEntityMap());
        builder.ApplyConfiguration(new QuestionEntityMap());
        builder.ApplyConfiguration(new QuestionnaireEntityMap());
        builder.ApplyConfiguration(new QuestionOptionEntityMap());
        builder.ApplyConfiguration(new UserStoryEntityMap());
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
