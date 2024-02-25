using System.Security.Claims;
using FishbowlSoftware.Planner.API.Middlewares;
using FishbowlSoftware.Planner.Application;
using FishbowlSoftware.Planner.Domain;
using FishbowlSoftware.Planner.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace FishbowlSoftware.Planner.API;

public static class Setup
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDomainLayer();
        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(builder.Configuration);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.ConfigureSwagger();
        builder.ConfigureLogger();
        builder.ConfigureCors();
        builder.ConfigureAuthentication();
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(GetSwaggerUIOptions(app.Configuration));
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
        var domain = $"https://{configuration["Auth0:Domain"]}/";
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
    }

    private static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;
        const string securityName = "OAuth2";
        
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(securityName, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"https://{configuration["Auth0:Domain"]}/authorize"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "Open ID" }
                        }
                    }
                }
            });
            
            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = securityName
                        },
                        Scheme = securityName,
                        Name = securityName,
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            };
            
            options.AddSecurityRequirement(securityRequirement);
        });
    }
    
    private static SwaggerUIOptions GetSwaggerUIOptions(IConfiguration configuration)
    {
        var options = new SwaggerUIOptions();
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fishbowl Software API");
        options.OAuthClientId(configuration["Auth0:SwaggerClientId"]);
        options.OAuthClientSecret(configuration["Auth0:SwaggerClientSecret"]);
        options.OAuthAppName("Fishbowl Software Swagger");
        options.OAuthScopeSeparator(" ");
        options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
        {
            { "audience", configuration.GetValue<string>("Auth0:Audience")! }
        });
        
        return options;
    }
}
