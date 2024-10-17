using discipline.core.Time.Abstractions;
using discipline.core.Time.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace discipline.core.Time.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddTime(this IServiceCollection services)
        => services.AddSingleton<IClock, Clock>();
}