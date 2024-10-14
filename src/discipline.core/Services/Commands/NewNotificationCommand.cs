namespace discipline.core.Services.Commands;

public sealed record NewNotificationCommand(Guid UserId, string Context, List<string> Parameters);