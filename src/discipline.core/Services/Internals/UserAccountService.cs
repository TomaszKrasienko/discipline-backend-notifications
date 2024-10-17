using discipline.core.Domain.UserAccounts.Entities;
using discipline.core.Persistence.Repositories.Abstractions;
using discipline.core.Services.Abstractions;
using discipline.core.Services.Commands;
using Microsoft.Extensions.Logging;

namespace discipline.core.Services.Internals;

internal sealed class UserAccountService(
    ILogger<UserAccountService> logger,
    IUserAccountRepository userAccountRepository) : IUserAccountService
{
    public async Task AddUserAsync(NewUserCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {0} for Id: {1}", nameof(AddUserAsync), command.UserId);
        var isExists = await userAccountRepository.IsExistsAsync(command.UserId, cancellationToken);
        if (isExists)
        {
            logger.LogWarning("User account already exists");
            return;
        }

        var userAccount = UserAccount.Create(command.UserId);
        await userAccountRepository.AddAsync(userAccount, cancellationToken);
    }
}