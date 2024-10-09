namespace discipline.core.Communication.Notifications.SignalR.Configuration;

public sealed record SignalROptions
{
    public string Route { get; set; }
    public string Method { get; set; }
}