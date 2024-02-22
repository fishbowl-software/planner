using FishbowlSoftware.Planner.Domain;
using FishbowlSoftware.Planner.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Okta.AspNetCore;
using Serilog;

namespace FishbowlSoftware.Planner.Identity;

internal static class Setup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDomainLayer();
        builder.Services.AddInfrastructureLayer(builder.Configuration);
        builder.Services.AddRazorPages();

        // builder.Services
        //     .AddIdentityServer(options =>
        //     {
        //         options.Events.RaiseErrorEvents = true;
        //         options.Events.RaiseInformationEvents = true;
        //         options.Events.RaiseFailureEvents = true;
        //         options.Events.RaiseSuccessEvents = true;
        //
        //         // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
        //         options.EmitStaticAudienceClaim = true;
        //     })
        //     .AddInMemoryIdentityResources(Config.IdentityResources)
        //     .AddInMemoryApiScopes(Config.ApiScopes)
        //     .AddInMemoryClients(Config.Clients)
        //     .AddAspNetIdentity<ApplicationUser>();

        // builder.Services.AddAuthentication()
        //     .AddGoogle(options =>
        //     {
        //         options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
        //
        //         // register your IdentityServer with Google at https://console.developers.google.com
        //         // enable the Google+ API
        //         // set the redirect URI to https://localhost:5001/signin-google
        //         options.ClientId = "copy client ID from Google here";
        //         options.ClientSecret = "copy client secret from Google here";
        //     });

        builder.ConfigureAuthentication();
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
        // app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();

        return app;
    }
    
    private static void ConfigureLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) =>
            lc.ReadFrom.Configuration(ctx.Configuration));
    }
    
    private static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            })
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOktaMvc(new OktaMvcOptions
            {
                OktaDomain = configuration.GetValue<string>("Okta:OktaDomain"),
                AuthorizationServerId = configuration.GetValue<string>("Okta:AuthorizationServerId"),
                ClientId = configuration.GetValue<string>("Okta:ClientId"),
                ClientSecret = configuration.GetValue<string>("Okta:ClientSecret"),
                Scope = new List<string> { "openid", "profile", "email" },
            });
    }
}
