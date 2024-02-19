using FishbowlSoftware.Planner.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FishbowlSoftware.Planner.Infrastructure.Builder
{
    internal class InfrastructureBuilder : ServiceCollection, IInfrastructureBuilder
    {
        private readonly IdentityBuilder _identityBuilder;

        internal InfrastructureBuilder(IdentityBuilder identityBuilder)
        {
            _identityBuilder = identityBuilder;
        }

        public IInfrastructureBuilder ConfigureIdentity(Action<IdentityBuilder> configure)
        {
            configure(_identityBuilder);
            return this;
        }

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
}
