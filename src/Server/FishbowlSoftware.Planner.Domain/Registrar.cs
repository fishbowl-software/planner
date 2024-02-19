using FishbowlSoftware.Planner.Domain.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FishbowlSoftware.Planner.Domain
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
