using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace FishbowlSoftware.Planner.API.StartupConfigurations;

public static class SwaggerConfiguration
{
    public static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;
        const string oauthScheme = "OAuth2";
        
        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(oauthScheme, new OpenApiSecurityScheme
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
                            Id = oauthScheme
                        },
                        Scheme = oauthScheme,
                        Name = oauthScheme,
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            };
            
            options.AddSecurityRequirement(securityRequirement);
        });
    }
    
    public static SwaggerUIOptions GetSwaggerUIOptions(IConfiguration configuration)
    {
        var options = new SwaggerUIOptions();
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Fishbowl Software API");
        options.OAuthClientId(configuration["Auth0:SwaggerClientId"]);
        options.OAuthClientSecret(configuration["Auth0:SwaggerClientSecret"]);
        options.OAuthAppName("Fishbowl Software Swagger");
        options.OAuthScopeSeparator(" ");
        options.OAuthAdditionalQueryStringParams(new Dictionary<string, string>
        {
            { "audience", configuration["Auth0:Audience"]! }
        });
        
        return options;
    }
}
