using discipline.core.Domain.NotificationDefinitions.Entities;

namespace discipline.core.Domain.NotificationDefinitions.Repositories;

public interface INotificationDefinitionRepository
{
    Task AddAsync(NotificationDefinition definition, CancellationToken cancellationToken);
    Task<bool> IsContextExistsAsync(string context, CancellationToken cancellationToken);
}