﻿using FishbowSoftware.Planner.Domain.Entities;
using FishbowSoftware.Planner.Domain.Persistence;
using FishbowSoftware.Planner.Infrastructure.Builder;
using FishbowSoftware.Planner.Infrastructure.Data;
using FishbowSoftware.Planner.Infrastructure.Interceptors;
using FishbowSoftware.Planner.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FishbowSoftware.Planner.Infrastructure
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
