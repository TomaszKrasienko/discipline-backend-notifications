using discipline.core.Communication.SignalR.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace discipline.core.Communication.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddCommunication(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddBroadcasting(configuration)
            .AddScoped<INotificationWrapper, BroadcastingSystemNotificationWrapper>();
}