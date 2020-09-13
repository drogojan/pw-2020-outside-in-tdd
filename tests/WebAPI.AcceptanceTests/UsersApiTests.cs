using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using WebAPI.AcceptanceTests.Models;
using Xunit;
using Xunit.Abstractions;

namespace WebAPI.AcceptanceTests
{
    public class UsersApiTests : AcceptanceTestBase
    {
        public UsersApiTests(AcceptanceTestWebApplicationFactory webApplicationFactory, ITestOutputHelper testOutputHelper) 
            : base(webApplicationFactory, testOutputHelper)
        {
        }

        [Fact]
        public async Task Should_Register_A_New_User()
        {
            var user = new
            {
                Username = "dragos.rogojan",
                Password = "pass123",
                About = "I like TDD"
            };
            var registerUserResponse = await HttpClient.PostAsJsonAsync("api/users", user);

            registerUserResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var registeredUser = await registerUserResponse.Content.ReadFromJsonAsync<RegisteredUser>();

            registeredUser.Id.Should().NotBeEmpty();
            registeredUser.Username.Should().Be(user.Username);
            registeredUser.About.Should().Be(user.About);
        }
    }
}