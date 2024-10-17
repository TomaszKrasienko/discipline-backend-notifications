using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace discipline.core.Initialization.Configuration;

internal static class Extensions
{
    private const string SectionName = "DefinitionsInitialization";
    
    internal static IServiceCollection AddInitialization(this IServiceCollection services, IConfiguration configuration)
        => services
            .Configure<InitializationOptions>(configuration.GetSection(SectionName))
            .AddHostedService<NotificationDefinitionsInitializer>();
}