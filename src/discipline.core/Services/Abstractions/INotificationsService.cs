using discipline.core.Services.Commands;

namespace discipline.core.Services.Abstractions;

public interface INotificationsService
{
    Task SendSystemNotification(NewSystemNotification newNotification, CancellationToken cancellationToken);
}