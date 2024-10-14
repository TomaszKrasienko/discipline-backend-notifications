using discipline.core.Domain.NotificationDefinitions.ValueObjects;
using discipline.core.Domain.SharedKernel.Types;

namespace discipline.core.Domain.NotificationDefinitions.Entities;

public sealed class NotificationDefinition
{
    public EntityId Id { get; set; }
    public Context? Context { get; private set; }
    public Title? Title { get; private set; }
    public Content? Content { get; private set; }

    //For mongo
    internal NotificationDefinition(Guid id, string context, string title, string content)
    {
        Id = id;
        Context = context;
        Title = title;
        Content = content;
    }
    
    private NotificationDefinition(Guid id)
        => Id = id;

    internal static NotificationDefinition Create(Guid id, string context, string title, string content)
    {
        var notificationStorage = new NotificationDefinition(id);
        notificationStorage.ChangeContext(context);
        notificationStorage.ChangeTitle(title);
        notificationStorage.ChangeContent(content);
        return notificationStorage;
    }

    private void ChangeContext(string value)
        => Context = new Context(value);

    private void ChangeTitle(string value)
        => Title = new Title(value);

    private void ChangeContent(string value)
        => Content = new Content(value);
}