using IdentityServer8;
using IdentityServer8.Models;

namespace YudaSPCWebApplication.BackendServer.IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];  

        public static IEnumerable<ApiScope> ApiScopes =>
        [
            new("api.qms", "QMS API")
        ];

        public static IEnumerable<ApiResource> ApiResources =>
        [
            new ApiResource("api.qms", "QMS API")
            {
                Scopes = { "api.qms" }
            }
        ];  

        public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "webportal",
                ClientName = "Web Portal Client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                RequireConsent = false,
                RequirePkce = true,
                AllowOfflineAccess = true,

                // Where to redirect after login
                RedirectUris = { "https://localhost:5001/signin-oidc" },

                // Where to redirect after logout   
                PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "api.qms" 
                }
            },
            new Client
            {
                ClientId = "swgger",
                ClientName = "Swagger Client",

                // no interactive user, use the clientid/secret for authentication
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                RequireConsent = false,
                RequirePkce = true,
                AllowOfflineAccess = true,

                // Where to redirect after login
                RedirectUris = { "https://localhost:5001/signin-oidc" },

                // Where to redirect after logout   
                PostLogoutRedirectUris = { "https://localhost:5001/signout-callback-oidc" },

                // secret for authentication
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                // scopes that client has access to
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api.qms"
                }
            }
        ];

        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddIdentityServer()
                .AddDeveloperSigningCredential()        //This is for dev only scenarios when you don’t have a certificate to use.
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);

            // omitted for brevity
        }
    }
}
