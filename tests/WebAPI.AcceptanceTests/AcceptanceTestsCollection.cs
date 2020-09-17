using Xunit;

namespace WebAPI.AcceptanceTests
{
    [CollectionDefinition("Acceptance Tests")]
    public class AcceptanceTestsCollection : ICollectionFixture<AcceptanceTestWebApplicationFactory>
    {
    }
}