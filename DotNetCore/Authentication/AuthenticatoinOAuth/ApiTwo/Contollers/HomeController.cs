using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiOne.Contollers
{
    public class HomeController: Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("/home")]
        public async Task<IActionResult> Index()
        {
            //retrieve access token for ApiOne
            var serverclient = _httpClientFactory.CreateClient();
            var discoveryDocument = await serverclient.GetDiscoveryDocumentAsync("https://localhost:44337/"); //identity server url
            var tokenResponse = await serverclient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest { 
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "ApiOne"
                });

            //---------------------------------------------------------------------------
            //retrieve secret data - use accesstoken to access ApiOne method to retrieve data
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            var apiOneUrl = "https://localhost:44331/";
            var response = await apiClient.GetAsync(apiOneUrl + "secret");

            var content = await response.Content.ReadAsStringAsync();

            return Ok(new
            {
                accss_token = tokenResponse.AccessToken,
                message = content
            }) ;
        }
    }
}
