namespace discipline.core.Communication.Notifications.SignalR.Types;

internal sealed record BroadcastingRoute(string Route, string Method,
    NotificationType Type);