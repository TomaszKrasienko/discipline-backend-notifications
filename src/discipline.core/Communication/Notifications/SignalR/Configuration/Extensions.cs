using discipline.core.Communication.Notifications.SignalR.Registry;
using discipline.core.Communication.Notifications.SignalR.Types;
using discipline.core.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace discipline.core.Communication.Notifications.SignalR.Configuration;

internal static class Extensions
{
    internal const string SectionName = "SignalR";
    
    internal static IServiceCollection AddBroadcasting(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSignalR();
        services.AddRegistry(configuration);
        services.AddScoped<IHubService, HubService>();
        return services;
    }

    private static IServiceCollection AddRegistry(this IServiceCollection services, IConfiguration configuration)
        => services.AddSingleton<IHubRegistry>(sp =>
        {
            var signalROptions = configuration.GetOptions<Dictionary<string, SignalROptions>>(SectionName);
            var registry = new HubRegistry();
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
        var options = app.Configuration.GetOptions<Dictionary<string, SignalROptions>>(SectionName);
        if (options.TryGetValue(NotificationType.System.ToString(), out var notificationsOptions))
        {
            app.MapHub<NotificationHub>(notificationsOptions.Route);
        }
        return app;
    }
}