using discipline.core.Domain.NotificationDefinitions.Entities;
using discipline.core.Domain.NotificationDefinitions.Repositories;
using discipline.core.Persistence.Abstractions;
using discipline.core.Persistence.Documents;
using discipline.core.Persistence.Documents.Mappers;
using MongoDB.Driver;

namespace discipline.core.Persistence.Repositories.Internal;

internal sealed class MongoNotificationDefinitionRepository(
    IDisciplineMongoCollection disciplineMongoCollection) : INotificationDefinitionRepository
{
    private readonly IMongoCollection<NotificationDefinitionDocument> _collection =
        disciplineMongoCollection.GetCollection<NotificationDefinitionDocument>();

    public async Task AddAsync(NotificationDefinition definition, CancellationToken cancellationToken)
        => await _collection.InsertOneAsync(definition.AsDocument(), null, cancellationToken);

    public async Task<bool> IsContextExistsAsync(string context, CancellationToken cancellationToken)
        => await _collection
            .Find(x => x.Context == context)
            .AnyAsync(cancellationToken);

    public async Task<NotificationDefinition> GetByIdAsync(string context, CancellationToken cancellationToken)
        => (await _collection
            .Find(x => x.Context == context)
            .FirstOrDefaultAsync(cancellationToken))?.AsEntity();
}