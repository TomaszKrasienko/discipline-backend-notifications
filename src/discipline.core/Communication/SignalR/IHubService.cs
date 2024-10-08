namespace discipline.core.Communication.SignalR;

public interface IHubService
{
    Task PublishForAll(NotificationType type, string message);
    Task PublishForUser(NotificationType type, Guid userId, string message);
}