using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using FreshingStore.Models.Models.Product;
using FreshingStore.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FreshingStore.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        //public async Task<IActionResult> Index()
        //{
        //    HttpClient client = _httpClientFactory.CreateClient();
        //    client.BaseAddress = new Uri("https://localhost:44345");
        //    var response = client.GetAsync("/api/products").Result;
        //    string jsonData = response.Content.ReadAsStringAsync()
        //                      .Result;
        //   var productDtos = JsonSerializer.Deserialize
        //                        <List<ProductDto>>(jsonData);
        //    var ProductViewModels = new ProductIndexViewModel(productDtos);
        //    return View(ProductViewModels);

        //}
            public async  Task<IActionResult> Index()
        {
            var HttpClient = _httpClientFactory.CreateClient("APIClient");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/products");






            var response = await HttpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var productDtos = await JsonSerializer.DeserializeAsync<List<ProductDto>>(responseStream);
                    var ProductViewModels = new ProductIndexViewModel(productDtos);
                    return View(ProductViewModels);
                }
            }

            throw new Exception("Problem access the API");

           
        }
    }
}
