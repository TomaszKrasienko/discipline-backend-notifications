using Microsoft.Extensions.DependencyInjection;

namespace discipline.core.Serializer.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddSerializer(this IServiceCollection services)
        => services
            .AddSingleton<ISerializer, NewtonsoftJsonSerializer>();
}