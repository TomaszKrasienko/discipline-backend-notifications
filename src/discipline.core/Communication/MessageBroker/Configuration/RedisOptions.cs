namespace discipline.core.Communication.MessageBroker.Configuration;

public sealed record RedisOptions
{
    public string ConnectionString { get; init; }
}