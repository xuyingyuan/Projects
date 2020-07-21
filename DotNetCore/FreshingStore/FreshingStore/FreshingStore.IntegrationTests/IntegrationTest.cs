using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using FreshingStore.API;
using FreshingStore.Repo.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace FreshingStore.IntegrationTests
{
    public class IntegrationTest
    {
        private readonly HttpClient Testclient;
        private readonly IServiceProvider _serviceProvider;
        private readonly string httpApiRoute =  "https://localhost:44345/";
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(AppDBContext));
                        services.AddDbContext<AppDBContext>(options => { options.UseInMemoryDatabase("TestDB"); });
                    });
                });
            _serviceProvider = appFactory.Services;
            Testclient = appFactory.CreateClient();
        }

        //[Fact]
        //public async Task GetProductAsync_ReturnProduct()
        //{
        //    //arrange
        //    var response = await Testclient.(apirout)

        //    //act

        //   //assert

        //}

        //[Fact]
        //protected async Task<GetResponse> CreateGetAsync(CreateGetRequest request)
        //{
        //    var url = httpApiRoute + "api/products";
        //    var response = await Testclient.GetAsync(url);
        //    return (await response.Content.ReadAsAsync<GetResponse>()).Data;
        //}
    }
}
