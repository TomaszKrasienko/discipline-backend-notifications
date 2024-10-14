using discipline.core.Domain.NotificationDefinitions.Entities;
using discipline.core.Domain.NotificationDefinitions.Repositories;
using discipline.core.Exceptions;
using discipline.core.Services.Abstractions;
using discipline.core.Services.Commands;

namespace discipline.core.Services.Internals;

internal sealed class NotificationDefinitionsService(
    INotificationDefinitionRepository notificationDefinitionRepository) : INotificationDefinitionsService
{
    public async Task AddNotificationDefinitionAsync(NewNotificationDefinitionCommand command, CancellationToken cancellationToken)
    {
        var isContextExists = await notificationDefinitionRepository.IsContextExistsAsync(command.Context,
            cancellationToken);

        if (isContextExists)
        {
            throw new ContextAlreadyRegisteredException(command.Context);
        }

        var notificationDefinition = NotificationDefinition.Create(command.Id, command.Context, command.Title,
            command.Content);
        await notificationDefinitionRepository.AddAsync(notificationDefinition, default);
    }
}