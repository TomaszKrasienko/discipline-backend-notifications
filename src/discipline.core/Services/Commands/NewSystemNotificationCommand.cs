namespace discipline.core.Services.Commands;

public sealed record NewSystemNotificationCommand(Guid UserId, string Context);