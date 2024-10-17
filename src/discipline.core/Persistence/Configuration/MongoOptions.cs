namespace discipline.core.Persistence.Configuration;

public sealed record MongoOptions
{
    public string ConnectionString { get; init; }
    public string Database { get; init; }
}