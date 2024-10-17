namespace discipline.core.Persistence.Abstractions;

public interface IMongoCollectionNameConvention
{
    
    string GetCollectionName<T>() where T : IDocument;
}