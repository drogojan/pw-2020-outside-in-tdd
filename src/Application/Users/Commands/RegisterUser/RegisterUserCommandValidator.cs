using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using OpenChat.Application.Common.Interfaces;

namespace OpenChat.Application.Users.Commands.RegisterUser
{
public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public RegisterUserCommandValidator(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(c => c.Username)
            .MustAsync(BeUniqueUsername).WithMessage("Username already in use.");
    }

    private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Users
            .AllAsync(u =>
                u.Username != username, cancellationToken);
    }
}
}