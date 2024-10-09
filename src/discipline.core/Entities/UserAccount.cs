using discipline.core.Exceptions;

namespace discipline.core.Entities;

public sealed class UserAccount
{
    private List<Notification> _notifications = new List<Notification>();
    public Guid UserId { get; }
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    private UserAccount(Guid userId)
        => UserId = userId;

    public static UserAccount Create(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new EmptyUserIdException();
        }
        return new UserAccount(userId);
    }

    public Guid AddNotification(string title,
        string content, DateTime createdAt)
    {
        var id = Guid.NewGuid();
        _notifications.Add(new Notification(id, title, content, createdAt));
        return id;
    }
}