namespace Services.Security.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope("PraxedesBackendUser", "Praxedes Backend User"),
                new ApiScope("PraxedesBackendPost", "Praxedes Backend Post")
            };

    public static IEnumerable<Client> Clients =>
        new Client[]
        { new Client
            {
                ClientId = "client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {
                    "PraxedesBackendUser",
                    "PraxedesBackendPost"
                }
            }
        };
}