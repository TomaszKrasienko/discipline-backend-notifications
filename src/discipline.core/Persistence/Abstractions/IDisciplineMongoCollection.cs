using MongoDB.Driver;

namespace discipline.core.Persistence.Abstractions;

public interface IDisciplineMongoCollection
{
    IMongoCollection<T> GetCollection<T>() where T : IDocument;
}