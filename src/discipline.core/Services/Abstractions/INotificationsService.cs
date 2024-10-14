using discipline.core.Services.Commands;

namespace discipline.core.Services.Abstractions;

public interface INotificationsService
{
    Task SendNotification(NewNotificationCommand command, CancellationToken cancellationToken);
}