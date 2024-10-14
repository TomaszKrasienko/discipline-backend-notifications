using discipline.core.Services.Commands;

namespace discipline.core.Services.Abstractions;

public interface INotificationsService
{
    Task SendSystemNotification(NewSystemNotificationCommand command, CancellationToken cancellationToken);
}