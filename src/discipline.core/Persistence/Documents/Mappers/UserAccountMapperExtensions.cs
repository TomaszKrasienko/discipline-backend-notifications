using discipline.core.Entities;

namespace discipline.core.Persistence.Documents.Mappers;

internal static class UserAccountMapperExtensions
{
    internal static UserAccountDocument AsDocument(this UserAccount entity)
        => new UserAccountDocument()
        {
            UserId = entity.UserId,
            Notifications = entity.Notifications?.Select(x => x.AsDocument())
        };

    private static NotificationDocument AsDocument(this Notification entity)
        => new NotificationDocument()
        {
            NotificationId = entity.NotificationId,
            Title = entity.Title,
            Content = entity.Content,
            CreatedAt = entity.CreatedAt,
            IsRead = entity.IsRead
        };

    private static UserAccount AsEntity(this UserAccountDocument document)
        => new UserAccount(document.UserId, null);

    private static Notification AsEntity(this NotificationDocument document)
        => new Notification();
}