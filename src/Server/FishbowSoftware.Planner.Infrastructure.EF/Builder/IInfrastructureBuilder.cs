using FishbowSoftware.Planner.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FishbowSoftware.Planner.Infrastructure.Builder
{
    public interface IInfrastructureBuilder : IServiceCollection
    {
        IInfrastructureBuilder ConfigureIdentity(Action<IdentityBuilder> configure);
        IInfrastructureBuilder ConfigureDatabase(Action<ApplicationDbContextOptions> configure);
    }
}
