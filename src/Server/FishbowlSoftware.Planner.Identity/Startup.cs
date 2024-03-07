using FishbowlSoftware.Planner.Domain;
using FishbowlSoftware.Planner.Identity.Configurations;
using FishbowlSoftware.Planner.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace FishbowlSoftware.Planner.Identity;

internal static class Startup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDomainLayer();
        builder.Services.AddInfrastructureLayer(builder.Configuration)
            .ConfigureIdentity(identityBuilder =>
            {
                identityBuilder
                    .AddSignInManager()
                    // .AddClaimsPrincipalFactory<UserCustomClaimsFactory>()
                    .AddDefaultTokenProviders();
            });
        
        builder.Services.AddRazorPages();
        
        builder.ConfigureAuthentication();
        builder.ConfigureCors();
        builder.ConfigureLogger();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors(app.Environment.IsDevelopment() ? "AnyCors" : "DefaultCors");
        
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}
