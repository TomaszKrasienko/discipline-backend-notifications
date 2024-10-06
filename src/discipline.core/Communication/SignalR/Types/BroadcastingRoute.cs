namespace discipline.core.Communication.SignalR.Types;

internal sealed record BroadcastingRoute(string Route, string Method,
    NotificationType Type);