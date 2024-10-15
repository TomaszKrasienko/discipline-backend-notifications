using discipline.core.Services.Commands;

namespace discipline.core.Services.Abstractions;

public interface INotificationsService
{
    Task SendNotificationAsync(NewNotificationCommand command, CancellationToken cancellationToken);
}