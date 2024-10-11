using discipline.core.Entities;

namespace discipline.core.Persistence.Repositories.Abstractions;

public interface IUserAccountRepository
{
    Task AddAsync(UserAccount userAccount, CancellationToken cancellationToken);
    Task<bool> IsExistsAsync(Guid userId, CancellationToken cancellationToken);
}