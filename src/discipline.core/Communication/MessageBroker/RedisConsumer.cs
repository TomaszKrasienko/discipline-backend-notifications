using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace discipline.core.Communication.MessageBroker;

public class RedisConsumer(
    ILogger<RedisConsumer> logger,
    IServiceProvider serviceProvider,
    string channel,
    Func<RedisChannel, RedisValue, Task> action) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();
        var subscriber = scope.ServiceProvider.GetRequiredService<ISubscriber>();
        await subscriber.SubscribeAsync(channel, (redisChannel, value) =>
        {
            action(redisChannel, value);
        });
    }
}