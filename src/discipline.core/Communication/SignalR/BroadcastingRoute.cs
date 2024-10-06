namespace discipline.core.Communication.SignalR;

internal sealed record BroadcastingRoute(string Route, string Method,
    BroadcastingType Type);