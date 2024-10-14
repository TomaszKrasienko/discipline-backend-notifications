using discipline.core.Domain.NotificationDefinitions.Entities;

namespace discipline.core.Persistence.Documents.Mappers;

public static class NotificationDefinitionDocumentMapperExtensions
{
    internal static NotificationDefinition AsEntity(this NotificationDefinitionDocument document)
        =>  new NotificationDefinition(document.Id, document.Context, document.Title, document.Content);

    internal static NotificationDefinitionDocument AsDocument(this NotificationDefinition entity)
        => new NotificationDefinitionDocument()
        {
            Id = entity.Id,
            Context = entity.Context,
            Title = entity.Title,
            Content = entity.Content?.Value, 
        };
}