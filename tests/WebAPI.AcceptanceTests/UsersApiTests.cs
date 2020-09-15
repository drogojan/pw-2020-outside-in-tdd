using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AspNetCore.Http.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OpenChat.Domain.Entities;
using WebAPI.AcceptanceTests.Models;
using Xunit;
using Xunit.Abstractions;
using static WebAPI.AcceptanceTests.Testing;

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

[Fact]
public async Task Should_Inform_When_A_Username_Is_Already_In_Use()
{
    var user = new
    {
        Username = "dragos.rogojan",
        Password = "pass123",
        About = "I already exist"
    };

    // backdoor manipulation
    await AddAsync(new User
    {
        Id = Guid.NewGuid(),
        Username = user.Username,
        Password = user.Password,
        About = user.Password
    });

    var response = await HttpClient.PostAsJsonAsync("api/users", user);

    response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    var errorDetails = await response.Content.ReadAsJsonAsync<ValidationProblemDetails>();

    errorDetails.Errors["Username"].Should().Contain("Username already in use.");
}
    }
}