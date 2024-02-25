using FishbowlSoftware.Planner.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FishbowlSoftware.Planner.Infrastructure.Builder;

internal class InfrastructureBuilder : ServiceCollection, IInfrastructureBuilder
{
    public IInfrastructureBuilder ConfigureDatabase(Action<ApplicationDbContextOptions> configure)
    {
        var options = new ApplicationDbContextOptions();
        configure(options);

        var serviceDesc = new ServiceDescriptor(typeof(ApplicationDbContextOptions), options);

        if (Contains(serviceDesc))
        {
            Remove(serviceDesc);
        }

        this.AddSingleton(options);
        return this;
    }
}
