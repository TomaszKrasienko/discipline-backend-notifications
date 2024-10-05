using discipline.core.Communication.SignalR.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace discipline.core.Configuration;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services
            .AddBroadcasting();
}