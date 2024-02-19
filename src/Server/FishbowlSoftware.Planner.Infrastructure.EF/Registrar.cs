using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Infrastructure.Builder;
using FishbowlSoftware.Planner.Infrastructure.Data;
using FishbowlSoftware.Planner.Infrastructure.Interceptors;
using FishbowlSoftware.Planner.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FishbowlSoftware.Planner.Infrastructure
{
    public static class Registrar
    {
        public static IInfrastructureBuilder AddInfrastructureLayer(
            this IServiceCollection services,
            IConfiguration configuration,
            string databaseConfigSection = "DatabaseConfig")
        {
            var identityBuilder = services.ConfigureIdentity();
            services.ConfigureDatabase(configuration, databaseConfigSection);
            services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
            return new InfrastructureBuilder(identityBuilder);
        }

        private static void ConfigureDatabase(
            this IServiceCollection services,
            IConfiguration configuration,
            string databaseConfigSection)
        {
            var dbContextOptions = configuration.GetRequiredSection(databaseConfigSection).Get<ApplicationDbContextOptions>()
                                   ?? throw new ArgumentException("Could not get a ApplicationDbContextOptions from the json configuration");

            services.AddSingleton(dbContextOptions);
            services.AddScoped<DispatchDomainEventsInterceptor>();
            services.AddScoped<AuditingEntitiesInterceptor>();
            services.AddDbContext<ApplicationDbContext>();
        }

        private static IdentityBuilder ConfigureIdentity(
            this IServiceCollection services)
        {
            return services.AddIdentityCore<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+\\";
                })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
        }
    }
}
