using FishbowlSoftware.Planner.API.Middlewares;
using FishbowlSoftware.Planner.Application;
using FishbowlSoftware.Planner.Domain;
using FishbowlSoftware.Planner.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Okta.AspNetCore;
using Serilog;

namespace FishbowlSoftware.Planner.API;

public static class Setup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDomainLayer();
        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(builder.Configuration);

        builder.Services.AddControllersWithViews();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.ConfigureLogger();
        builder.ConfigureCors();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(app.Environment.IsDevelopment() ? "AnyCors" : "DefaultCors");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCustomExceptionHandler();
        app.MapControllers();
        return app;
    }

    private static void ConfigureLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((ctx, lc) =>
            lc.ReadFrom.Configuration(ctx.Configuration));
    }

    private static void ConfigureCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("DefaultCors", cors =>
            {
                cors.WithOrigins(
                        "https://{your production website}.com",
                        "https://*.{your production website}.com")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
            options.AddPolicy("AnyCors", cors =>
            {
                cors.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
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
