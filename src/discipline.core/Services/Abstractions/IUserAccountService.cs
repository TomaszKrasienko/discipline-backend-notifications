using discipline.core.Services.Commands;

namespace discipline.core.Services.Abstractions;

public interface IUserAccountService
{
    Task AddUserAsync(NewUserCommand command, CancellationToken cancellationToken);
}