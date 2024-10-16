namespace discipline.core.Domain.UserAccounts.Entities;

public sealed class Notification
{
    public Guid NotificationId { get; }
    public string Title { get;}
    public string Content { get; }
    public DateTime CreatedAt { get; }
    public bool IsRead { get; private set; }

    //For mongo
    internal Notification(Guid notificationId, string title, string content, DateTime createdAt, bool isRead)
    {
        NotificationId = notificationId;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        IsRead = isRead;
    }

    internal Notification(Guid notificationId, string title,
        string content, DateTime createdAt)
    {
        NotificationId = notificationId;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        IsRead = false;
    }
}