using discipline.core.Domain.NotificationsStorage.ValueObjects;
using discipline.core.Domain.SharedKernel;

namespace discipline.core.Domain.NotificationsStorage.Entities;

public sealed class NotificationStorage
{
    public Guid Id { get; set; }
    public Context Context { get; private set; }
    public Title Title { get; private set; }
    public Content Content { get; private set; }

    private NotificationStorage(Guid id)
        => Id = id;

    internal static NotificationStorage Create(Guid id, string context, string title, string content)
    {
        var notificationStorage = new NotificationStorage(id);
        return notificationStorage;
    }

    private void ChangeContext(string value)
        => Context = new Context(value);

    private void ChangeTitle(string value)
        => Title = new Title(value);

    private void ChangeContent(string value)
        => Content = new Content(value);
}