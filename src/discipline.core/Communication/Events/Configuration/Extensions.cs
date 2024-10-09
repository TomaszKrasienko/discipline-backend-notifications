using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace discipline.core.Communication.Events.Configuration;

internal static class Extensions
{
    internal const string SectionName = "Redis";

    internal static IServiceCollection AddEvents(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions(configuration)
            .AddSingleton(sp =>
            {
                var redisOptions = sp.GetRequiredService<IOptions<RedisOptions>>().Value;
                return ConnectionMultiplexer.Connect(redisOptions.ConnectionString);
            })
            .AddScoped(sp =>
            {
                var connection = sp.GetRequiredService<ConnectionMultiplexer>();
                return connection.GetSubscriber();
            });
        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<RedisOptions>(configuration.GetSection(SectionName));
}