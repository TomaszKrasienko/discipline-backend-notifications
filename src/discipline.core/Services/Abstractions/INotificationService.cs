using discipline.core.Services.Commands;

namespace discipline.core.Services.Abstractions;

public interface INotificationService
{
    Task SendNotification(NewNotification newNotification);
}