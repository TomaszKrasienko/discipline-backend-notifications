using discipline.core.Persistence.Repositories.Abstractions;
using discipline.core.Serializer;
using discipline.core.Services.Abstractions;
using discipline.core.Services.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace discipline.core.Communication.MessageBroker.Configuration;

internal static class Extensions
{
    internal const string SectionName = "Redis";

    internal static IServiceCollection AddRedisMessageBroker(this IServiceCollection services, IConfiguration configuration)
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
        services.AddConsumers();
        return services;
    }

    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<RedisOptions>(configuration.GetSection(SectionName));

    private static IServiceCollection AddConsumers(this IServiceCollection services)
        => services.AddHostedService(sp =>
        {
            var channel = "new-user";
            var logger = sp.GetRequiredService<ILogger<RedisConsumer>>();
            return new RedisConsumer(logger, sp, channel, async (redisChannel, value) =>
            {
                logger.LogInformation($"Getting message for: {channel}");
                var scope = sp.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IUserAccountService>();
                var serializer = scope.ServiceProvider.GetRequiredService<ISerializer>();
                var command = serializer.ToObject<NewUserCommand>(value);
                await service.AddUserAsync(command, default);
            });
        });
}