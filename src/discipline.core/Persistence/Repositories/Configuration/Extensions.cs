using discipline.core.Domain.NotificationDefinitions.Repositories;
using discipline.core.Persistence.Repositories.Abstractions;
using discipline.core.Persistence.Repositories.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace discipline.core.Persistence.Repositories.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddRepositories(this IServiceCollection services)
        => services
            .AddScoped<IUserAccountRepository, MongoUserAccountRepository>()
            .AddScoped<INotificationDefinitionRepository, MongoNotificationDefinitionRepository>();
}