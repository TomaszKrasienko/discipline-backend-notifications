using discipline.core.Persistence.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace discipline.core.Persistence.Documents;

public sealed class NotificationDefinitionDocument : IDocument
{
    [BsonId]
    [BsonElement]
    public Guid Id { get; set; }
    
    [BsonElement("context")]
    public string Context { get; set; }
    
    [BsonElement("title")]
    public string Title { get; set; }
    
    [BsonElement("content")]
    public string Content { get; set; }
}