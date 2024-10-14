namespace discipline.core.Services.Commands;

public sealed record NewNotificationDefinitionCommand(Guid Id, string Context, string Title, string Content);