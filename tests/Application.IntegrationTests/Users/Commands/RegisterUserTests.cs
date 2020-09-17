using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using OpenChat.Application.Common.Interfaces;
using OpenChat.Application.Users.Commands.RegisterUser;
using Xunit;

namespace Application.IntegrationTests.Users.Commands
{
    public class RegisterUserTests : IntegrationTestBase
    {
        [Fact]
        public async Task Should_Register_A_New_User()
        {
            Guid userId = Guid.NewGuid();
            IApplicationDbContext dbContext = Testing.GetService<IApplicationDbContext>();

            Mock<IGuidGenerator> guidGeneratorStub = new Mock<IGuidGenerator>();
            guidGeneratorStub.Setup(m => m.GetNextAsync()).ReturnsAsync(userId);

            var sut = new RegisterUserCommandHandler(guidGeneratorStub.Object, dbContext);

            RegisterUserCommand request = new RegisterUserCommand()
            {
                Username = "dragos.rogojan",
                Password = "pass123",
                About = "I like TDD"
            };

            var registeredUserVm = await sut.Handle(request, CancellationToken.None);

            // output verification
            registeredUserVm.Id.Should().Be(userId);
            registeredUserVm.Username.Should().Be(request.Username);
            registeredUserVm.About.Should().Be(request.About);

            // state verification
            var registeredUser = await dbContext.Users.SingleAsync(u => u.Id == userId);
            registeredUser.Username.Should().Be(request.Username);
            registeredUser.Password.Should().Be(request.Password);
            registeredUser.About.Should().Be(request.About);
        }
    }
}