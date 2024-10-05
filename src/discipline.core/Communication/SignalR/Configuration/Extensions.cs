using Microsoft.Extensions.DependencyInjection;

namespace discipline.core.Communication.SignalR.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddBroadcasting(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }
}