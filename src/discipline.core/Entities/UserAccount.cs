using discipline.core.Exceptions;

namespace discipline.core.Entities;

public sealed class UserAccount
{
    private readonly List<Notification> _notifications = [];
    public Guid UserId { get; }
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    //For mongo
    internal UserAccount(Guid userId, List<Notification> notifications)
    {
        UserId = userId;
        _notifications = notifications ?? [];
    }

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