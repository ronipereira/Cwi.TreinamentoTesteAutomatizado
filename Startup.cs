using BoDi;
using Cwi.TreinamentoTesteAutomatizado.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Cwi.TreinamentoTesteAutomatizado
{
    [Binding]
    public class Startup
    {
        private readonly IObjectContainer ObjectContainer;

        public Startup(IObjectContainer objectContainer)
        {
            ObjectContainer = objectContainer;
        }

        [BeforeScenario]
        public void DependecyInjection()
        {
            var configuration = GetConfiguration();
            var httpRequestController = new HttpRequestController(GetHttpClientFactory(), configuration["ExampleApiUrl"]);

            ObjectContainer.RegisterInstanceAs<HttpRequestController>(httpRequestController);
        }

        private IConfiguration GetConfiguration()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            return new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .AddJsonFile($"appsettings.{envName}.json", optional: true).Build();
                       
        }

        private IHttpClientFactory GetHttpClientFactory()
        {
            return new ServiceCollection()
                        .AddHttpClient()
                        .BuildServiceProvider()
                        .GetRequiredService<IHttpClientFactory>();

        }
    }
}
