namespace discipline.core.Communication.SignalR;

internal interface IRoutesRegistry
{
    void Add(BroadcastingRoute broadcastingRoute);
    BroadcastingRoute Get(BroadcastingType type);
}

internal sealed class RoutesRegistry : IRoutesRegistry
{
    private readonly Dictionary<BroadcastingType, BroadcastingRoute> _routes = new ();

    public void Add(BroadcastingRoute broadcastingRoute)
        => _routes.TryAdd(broadcastingRoute.Type, broadcastingRoute);

    public BroadcastingRoute Get(BroadcastingType type)
        => _routes.GetValueOrDefault(type);
}