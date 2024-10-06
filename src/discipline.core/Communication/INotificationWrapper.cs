using discipline.core.Communication.SignalR.Types;

namespace discipline.core.Communication;

public interface INotificationWrapper
{
    Task Send(NotificationType type);
    bool CanByApplied(NotificationType type);
}