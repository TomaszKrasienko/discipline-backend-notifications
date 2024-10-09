namespace discipline.core.Entities;

public sealed class Notification
{
    public Guid NotificationId { get; }
    public string Title { get;}
    public string Content { get; }
    public DateTime CreatedAt { get; }
    public bool IsRead { get; private set; }

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