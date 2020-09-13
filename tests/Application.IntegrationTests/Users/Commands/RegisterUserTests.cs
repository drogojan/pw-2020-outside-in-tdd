using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using OpenChat.Application.Common.Interfaces;
using OpenChat.Application.Users.Commands.RegisterUser;
using Xunit;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Users.Commands
{
    public class RegisterUserTests : IntegrationTestBase
    {
        [Fact]
        public async Task Should_Register_A_New_User()
        {
            Mock<IGuidGenerator> guidGeneratorStub = new Mock<IGuidGenerator>();
            Guid newUserId = Guid.NewGuid();
            guidGeneratorStub.Setup(m => m.GetNextAsync()).ReturnsAsync(newUserId);
            IApplicationDbContext dbContext = GetService<IApplicationDbContext>();

            var sut = new RegisterUserCommandHandler(guidGeneratorStub.Object, dbContext);

            RegisterUserCommand request = new RegisterUserCommand()
            {
                Username = "dragos.rogojan",
                Password = "pass123",
                About = "I like TDD"
            };
            var registeredUserVm = await sut.Handle(request, CancellationToken.None);

            // output verification
            registeredUserVm.Id.Should().NotBeEmpty();
            registeredUserVm.Username.Should().Be(request.Username);
            registeredUserVm.About.Should().Be(request.About);

            // state verification
            var dbUser = await dbContext.Users.SingleAsync(u => u.Id == newUserId);

            dbUser.Username.Should().Be(request.Username);
            dbUser.Password.Should().Be(request.Password);
            dbUser.About.Should().Be(request.About);
        }
    }
}
