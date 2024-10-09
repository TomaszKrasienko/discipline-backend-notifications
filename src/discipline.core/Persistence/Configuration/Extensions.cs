using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace discipline.core.Persistence.Configuration;

internal static class Extensions
{
    private const string SectionName = "Mongo";

    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddOptions(configuration)
            .AddConnection();
    
    private static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration configuration)
        => services.Configure<MongoOptions>(configuration.GetSection(SectionName));

    private static IServiceCollection AddConnection(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return new MongoClient(options.ConnectionString);
        });

        services.AddTransient(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.Database);
        });
        return services;
    }
}