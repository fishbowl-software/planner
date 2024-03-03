using Duende.IdentityServer.Models;

namespace FishbowlSoftware.Planner.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new[]
        {
            new ApiScope("planner.read"),
            new ApiScope("planner.write"),
        };
    
    public static IEnumerable<ApiResource> ApiResources =>
        new[]
        {
            new ApiResource("planner.api", "Planner API")
            {
                Scopes = { "planner.read", "planner.write" }
            }
        };

    public static IEnumerable<Client> Clients =>
        new[]
        {
            new Client
            {
                ClientId = "planner.swagger",
                ClientName = "Planner Swagger UI",
                AllowedGrantTypes = GrantTypes.ClientCredentials, // m2m client credentials flow client
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                AllowedScopes = { "planner.read", "planner.write" }
            },
            
            new Client
            {
                ClientId = "planner.spa",
                ClientName = "Planner SPA App",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code, // interactive client using code flow + pkce
                RedirectUris = { "http://localhost:7002" },
                PostLogoutRedirectUris = { "http://localhost:7002" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "email", "planner.read", "planner.write" }
            },
        };
}
