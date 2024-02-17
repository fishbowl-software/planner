using FishbowSoftware.Planner.Domain.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FishbowSoftware.Planner.Domain
{
    public static class Registrar
    {
        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Registrar).Assembly));
            services.AddScoped<AuthorizedUserContext>();
            return services;
        }
    }
}
