using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace IdentityServer.Statics
{
    public static class Configuration
    {
        public static IEnumerable<IdentityResource> GetIdentityResource() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),

                //add claims here
                new IdentityResource
                {
                   Name = "SueClaim.scope",
                   UserClaims =
                    {
                        "sue.claimOne"
                      
                    }
                }
            };

        public static IEnumerable<ApiResource> GetApis() =>           
                new List<ApiResource>
                {
                    //Note: the claims set up here is actually shared by both ApiOne and ApiTwo resources if client have both resource in scope
                    new ApiResource("ApiOne", new string[]{"sue.api.claimOne" }),
                    new ApiResource("ApiTwo", new string[]{"sue.api.claimTwo" })    
                };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client{
                ClientId = "client_id",
                ClientSecrets = { new Secret("client_secret".ToSha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"ApiOne"},
               },
                 new Client{
                ClientId = "client_id_mvc",
                ClientSecrets = { new Secret("client_secret_mvc".ToSha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "https://localhost:44382/signin-oidc" },
                AllowedScopes = {"ApiOne", "ApiTwo", 
                         IdentityServerConstants.StandardScopes.OpenId,
                     IdentityServerConstants.StandardScopes.Profile,
                    "SueClaim.scope"},


                //AlwaysIncludeUserClaimsInIdToken = true,    //set to true will put all teh claims in the id Token

                RequireConsent = false //do not show consent page for this client

               },
               
            };



    }
}
