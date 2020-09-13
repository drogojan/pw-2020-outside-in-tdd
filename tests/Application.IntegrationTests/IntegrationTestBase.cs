using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests
{
    using static Testing;

    [Collection("Integration Tests")]
    public class IntegrationTestBase : IAsyncLifetime
    {
        public async Task InitializeAsync()
        {
            await ResetState();
        }

        public async Task DisposeAsync() => await Task.CompletedTask;
    }
}