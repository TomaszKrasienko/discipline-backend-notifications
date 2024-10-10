using discipline.core.Persistence.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace discipline.core.Persistence.Documents;

public sealed class UserAccountDocument : IDocument
{
    [BsonId]
    [BsonElement("userId")]
    public Guid UserId { get; set; }
}

public sealed class NotificationDocument : IDocument
{
    [BsonId]
    [BsonElement("notificationId")]
    public Guid NotificationId { get; set; }
    
    [BsonElement("title")]
    public string Title { get; set; }
    
    [BsonElement("content")]
    public string Content { get; set; }
    
    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }
    
    [BsonElement("isRead")]
    public bool IsRead { get; set; }
}