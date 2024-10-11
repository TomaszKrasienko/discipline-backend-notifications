using discipline.core.Services.Abstractions;
using discipline.core.Services.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace discipline.core.Services.Configuration;

internal static class Extensions
{
    internal static IServiceCollection AddServices(this IServiceCollection services)
        => services.AddScoped<IUserAccountService, UserAccountService>();
}