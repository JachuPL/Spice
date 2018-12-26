using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Spice.WebAPI.Tests.Common
{
    internal abstract class AbstractIntegrationTestsBaseFixture
    {
        private WebApplicationFactory<Startup> ApplicationFactory { get; }
        protected HttpClient Client { get; }

        protected AbstractIntegrationTestsBaseFixture()
        {
            ApplicationFactory = new WebApplicationFactory<Startup>().WithWebHostBuilder(CustomWebHostBuilder);
            Client = ApplicationFactory.CreateClient();
        }

        private void CustomWebHostBuilder(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(ServicesConfiguration);
        }

        protected abstract void ServicesConfiguration(IServiceCollection services);
    }
}