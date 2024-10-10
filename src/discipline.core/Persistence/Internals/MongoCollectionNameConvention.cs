using discipline.core.Persistence.Abstractions;

namespace discipline.core.Persistence.Internals;

internal sealed class MongoCollectionNameConvention : IMongoCollectionNameConvention
{
    public string GetCollectionName<T>() where T : IDocument
        => $"{typeof(T).Name}s";
}