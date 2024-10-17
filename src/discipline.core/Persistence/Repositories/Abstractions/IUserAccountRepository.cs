using discipline.core.Domain.UserAccounts.Entities;

namespace discipline.core.Persistence.Repositories.Abstractions;

public interface IUserAccountRepository
{
    Task AddAsync(UserAccount userAccount, CancellationToken cancellationToken);
    Task UpdateAsync(UserAccount userAccount, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserAccount> GetByIdAsync(Guid userId, CancellationToken cancellationToken);
}