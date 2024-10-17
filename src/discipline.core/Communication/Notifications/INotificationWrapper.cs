namespace discipline.core.Communication.Notifications;

public interface INotificationWrapper
{
    Task SendForAll(object message);
    Task SendForUser(Guid userId, object message);
    bool CanByApplied(NotificationType type);
}