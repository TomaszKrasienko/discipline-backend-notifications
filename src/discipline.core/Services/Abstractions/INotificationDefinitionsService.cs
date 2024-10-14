using discipline.core.Services.Commands;

namespace discipline.core.Services.Abstractions;

public interface INotificationDefinitionsService
{
    Task AddNotificationDefinitionAsync(NewNotificationDefinitionCommand command, CancellationToken cancellationToken);
}