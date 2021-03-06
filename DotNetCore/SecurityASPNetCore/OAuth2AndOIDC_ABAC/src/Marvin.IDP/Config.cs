﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Marvin.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "your role(s)", new List<string>(){"role"}), //scop, displayname, list of value of return

                //use ABAC --------------------------
                new IdentityResource("country",
                    "The Country you are living in",
                    new List<string>(){"country"}),
                new IdentityResource("subscriptionlevel"
                    , "Your subscription level",
                    new List<string>(){"subscriptionlevel"}                    
                    )
            };

        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
               new ApiResource("imagegalleryapi", "Image Gallery API",
                   new List<string>(){"role"})
               {
                   ApiSecrets = {new Secret("apisecret".Sha256())}
               }       
            };
        
        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    AccessTokenType = AccessTokenType.Reference,

                    //here are 3 setup token life time
                    //IdentityTokenLifetime=3000, // defautl is 5 min.
                    //AuthorizationCodeLifetime = 3000, //default is 5 min
                    //AccessTokenLifetime = 60000, //default is 1 hour
                    AccessTokenLifetime = 120,
                    //set up re-fresh token
                    AllowOfflineAccess = true,
                    //AbsoluteRefreshTokenLifetime=60000, //defaut is 30 days
                    //RefreshTokenExpiration = TokenExpiration.Sliding,
                    //SlidingRefreshTokenLifetime =  ???
                    UpdateAccessTokenClaimsOnRefresh = true, //get updated if claim changes


                    ClientName = "Image Gallary",
                    ClientId="ImageGallaryClient1",
                    AllowedGrantTypes=GrantTypes.Code,
                    RequirePkce = true,
                    RedirectUris=new List<string>()
                    {
                        "https://localhost:44389/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44389/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "imagegalleryapi",
                        "country",
                        "subscriptionlevel"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            
            };
        
    }
}