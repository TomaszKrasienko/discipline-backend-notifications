using discipline.core.Communication.SignalR.Types;

namespace discipline.core.Communication;

public interface INotificationWrapper
{
    Task SendForAll(object message);
    bool CanByApplied(NotificationType type);
}