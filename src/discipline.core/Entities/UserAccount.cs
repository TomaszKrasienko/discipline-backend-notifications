namespace discipline.core.Entities;

public sealed class UserAccount
{
    private List<Notification> _notifications = new List<Notification>();
    public Guid UserId { get; }
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    private UserAccount(Guid userId)
        => UserId = userId;

    public static UserAccount Create(Guid userId)
        => new UserAccount(userId);
}