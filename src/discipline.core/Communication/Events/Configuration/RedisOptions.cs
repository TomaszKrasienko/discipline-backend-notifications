namespace discipline.core.Communication.Events.Configuration;

public sealed record RedisOptions
{
    public string ConnectionString { get; init; }
}