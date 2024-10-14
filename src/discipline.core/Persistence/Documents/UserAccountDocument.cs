using discipline.core.Persistence.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace discipline.core.Persistence.Documents;

public sealed class UserAccountDocument : IDocument
{
    [BsonId]
    [BsonElement("userId")]
    public Guid UserId { get; set; }

    [BsonElement("notifications")]
    public IEnumerable<NotificationDocument>? Notifications { get; set; }
}