using discipline.core.Communication.SignalR.Types;

namespace discipline.core.Communication;

public interface INotificationWrapper
{
    Task Send(object message);
    bool CanByApplied(NotificationType type);
}