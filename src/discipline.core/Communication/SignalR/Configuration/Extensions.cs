using discipline.core.Communication.SignalR.Registry;
using discipline.core.Communication.SignalR.Types;
using discipline.core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace discipline.core.Communication.SignalR.Configuration;

internal static class Extensions
{
    private const string SectionName = "SignalR";
    
    internal static IServiceCollection AddBroadcasting(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }

    private static IServiceCollection AddRegistry(this IServiceCollection services, IConfiguration configuration)
        => services.AddSingleton<IRoutesRegistry>(sp =>
        {
            var signalROptions = configuration.GetOptions<Dictionary<string, SignalROptions>>(SectionName);
            var registry = new RoutesRegistry();
            foreach (var option in signalROptions)    
            {
                if (!Enum.TryParse(option.Key, out NotificationType type))
                {
                    throw new ArgumentException();
                }
                registry.Add(new BroadcastingRoute(option.Value.Route,
                    option.Value.Method, type));
            }

            return registry;
        });


    

    internal static WebApplication UseBroadcasting(this WebApplication app)
    {
        return app;
    }
}