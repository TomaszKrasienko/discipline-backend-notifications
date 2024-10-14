using discipline.core.Domain.UserAccounts.Entities;
using discipline.core.Persistence.Abstractions;
using discipline.core.Persistence.Documents;
using discipline.core.Persistence.Documents.Mappers;
using discipline.core.Persistence.Repositories.Abstractions;
using MongoDB.Driver;

namespace discipline.core.Persistence.Repositories.Internal;

internal sealed class MongoUserAccountRepository(
    IDisciplineMongoCollection disciplineMongoCollection) : IUserAccountRepository
{
    private readonly IMongoCollection<UserAccountDocument> _collection =
        disciplineMongoCollection.GetCollection<UserAccountDocument>();

    public async Task AddAsync(UserAccount userAccount, CancellationToken cancellationToken)
        => await _collection.InsertOneAsync(userAccount.AsDocument(), null, cancellationToken);

    public async Task<bool> IsExistsAsync(Guid userId, CancellationToken cancellationToken)
        => await _collection
            .Find(x => x.UserId == userId)
            .AnyAsync(cancellationToken);

    public async Task<UserAccount> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
        => (await _collection
            .Find(x => x.UserId == userId)
            .FirstOrDefaultAsync(cancellationToken)).AsEntity();
}