using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace discipline.core.Communication.Events;

public class RedisSubscriber(
    ILogger<RedisSubscriber> logger,
    ISubscriber subscriber) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await subscriber.SubscribeAsync("new-user", (channel, message) =>
        {
            logger.LogInformation("Getting message from channel: {0}", channel);
        });
    }
}