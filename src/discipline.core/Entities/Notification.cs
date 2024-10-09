namespace discipline.core.Entities;

public sealed class Notification
{
    public Guid NotificationId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
}