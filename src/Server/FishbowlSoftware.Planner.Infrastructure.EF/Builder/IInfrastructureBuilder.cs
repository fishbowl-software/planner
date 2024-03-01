using FishbowlSoftware.Planner.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FishbowlSoftware.Planner.Infrastructure.Builder;

public interface IInfrastructureBuilder : IServiceCollection
{
    IInfrastructureBuilder ConfigureIdentity(Action<IdentityBuilder> configure);
    IInfrastructureBuilder ConfigureDatabase(Action<ApplicationDbContextOptions> configure);
}
