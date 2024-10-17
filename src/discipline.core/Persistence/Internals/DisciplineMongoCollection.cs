using discipline.core.Persistence.Abstractions;
using MongoDB.Driver;

namespace discipline.core.Persistence.Internals;

internal sealed class DisciplineMongoCollection(
    IMongoDatabase mongoDatabase,
    IMongoCollectionNameConvention mongoCollectionNameConvention) : IDisciplineMongoCollection
{
    public IMongoCollection<T> GetCollection<T>() where T : IDocument
    {
        var collectionName = mongoCollectionNameConvention.GetCollectionName<T>();
        return mongoDatabase.GetCollection<T>(collectionName);
    }
}