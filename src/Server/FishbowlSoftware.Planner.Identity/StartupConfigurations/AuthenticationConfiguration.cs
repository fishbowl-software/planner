using Auth0.AspNetCore.Authentication;

namespace FishbowlSoftware.Planner.Identity.StartupConfigurations;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;
        
        services.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = configuration.GetValue<string>("Auth0:Domain")!;
            options.ClientId = configuration.GetValue<string>("Auth0:ClientId")!;
        });
    }
}
