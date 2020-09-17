using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using static WebAPI.AcceptanceTests.Testing;

namespace WebAPI.AcceptanceTests
{
    [Collection("Acceptance Tests")]
    public class AcceptanceTestBase : IAsyncLifetime
    {
        protected static HttpClient HttpClient;

        public AcceptanceTestBase(AcceptanceTestWebApplicationFactory webApplicationFactory, ITestOutputHelper testOutputHelper)
        {
            webApplicationFactory.TestOutputHelper = testOutputHelper;
            HttpClient ??= webApplicationFactory.CreateClient();
        }

        public async Task InitializeAsync()
        {
            await ResetState();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }
    }
}