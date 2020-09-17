using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace WebAPI.AcceptanceTests
{
    using static Testing;

    public class AcceptanceTestWebApplicationFactory : WebApplicationFactory<WebAPI.Startup>
    {
        public ITestOutputHelper TestOutputHelper { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(configurationBuilder =>
            {
                configurationBuilder
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .AddEnvironmentVariables();
                Configuration = configurationBuilder.Build();
            });

            builder.ConfigureServices(services =>
            {
                services.AddLogging(loggingBuilder => loggingBuilder.AddXUnit(TestOutputHelper));

                var serviceProvider = services.BuildServiceProvider();
                ScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();

                EnsureDatabase();
            });
        }
    }
}