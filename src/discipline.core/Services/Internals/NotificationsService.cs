using discipline.core.Communication.Notifications;
using discipline.core.Persistence.Repositories.Abstractions;
using discipline.core.Services.Abstractions;
using discipline.core.Services.Commands;
using discipline.core.Time.Abstractions;
using Microsoft.Extensions.Logging;

namespace discipline.core.Services.Internals;

internal sealed class NotificationsService(
    ILogger<NotificationsService> logger,
    IUserAccountRepository userAccountRepository,
    IClock clock,
    INotificationWrapper notificationWrapper) : INotificationsService
{
    public async Task SendSystemNotification(NewSystemNotification newNotification, CancellationToken cancellationToken)
    {
        var userAccount = await userAccountRepository
            .GetByIdAsync(newNotification.UserId, cancellationToken);
        if (userAccount is null)
        {
            logger.LogWarning("User account with ID: {0} not found", newNotification.UserId);
            return;
        }
        
        
    }
}