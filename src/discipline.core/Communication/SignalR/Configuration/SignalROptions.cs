namespace discipline.core.Communication.SignalR.Configuration;

public sealed record SignalROptions
{
    public string Route { get; set; }
    public string Method { get; set; }
}