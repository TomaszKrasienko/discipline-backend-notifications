using discipline.core.Communication.Notifications;
using discipline.core.Domain.NotificationDefinitions.Repositories;
using discipline.core.Persistence.Repositories.Abstractions;
using discipline.core.Services.Abstractions;
using discipline.core.Services.Commands;
using discipline.core.Time.Abstractions;
using Microsoft.Extensions.Logging;

namespace discipline.core.Services.Internals;

internal sealed class NotificationsService(
    ILogger<NotificationsService> logger,
    IUserAccountRepository userAccountRepository,
    INotificationDefinitionRepository notificationDefinitionRepository,
    IClock clock,
    INotificationWrapper notificationWrapper) : INotificationsService
{
    public async Task SendNotification(NewNotificationCommand command, CancellationToken cancellationToken)
    {
        var userAccount = await userAccountRepository
            .GetByIdAsync(command.UserId, cancellationToken);
        if (userAccount is null)
        {
            logger.LogWarning("User account with ID: {0} not found", command.UserId);
            return;
        }

        var notificationDefinition = await notificationDefinitionRepository
            .GetByIdAsync(command.Context, cancellationToken);
        if (notificationDefinition is null)
        {
            logger.LogWarning($"Notification definition with context: {command.Context} not found");
            return;
        }
        
        
    }
}