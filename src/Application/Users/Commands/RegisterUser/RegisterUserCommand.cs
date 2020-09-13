using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OpenChat.Application.Common.Interfaces;
using OpenChat.Domain.Entities;

namespace OpenChat.Application.Users.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<RegisteredUserVm>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisteredUserVm>
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IApplicationDbContext _dbContext;

        public RegisterUserCommandHandler(IGuidGenerator guidGenerator, IApplicationDbContext dbContext)
        {
            _guidGenerator = guidGenerator;
            _dbContext = dbContext;
        }

        public async Task<RegisteredUserVm> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var id = await _guidGenerator.GetNextAsync();

            var user = new User()
            {
                Id = id,
                Username = request.Username,
                Password = request.Password,
                About = request.About
            };

            await _dbContext.Users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new RegisteredUserVm()
            {
                Id = user.Id,
                Username = user.Username,
                About = user.About
            };
        }
    }
}