using discipline.core.Communication.Configuration;
using discipline.core.Communication.SignalR.Configuration;
using discipline.core.Serializer.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace discipline.core.Configuration;

public static class Extensions
{
    private const string CorsName = "discipline-notifications-cors";
    
    public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddCommunication(configuration)
            .AddSerializer()
            .AddDisciplineCors();

    private static IServiceCollection AddDisciplineCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(CorsName, policy =>
            {
                policy
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithExposedHeaders("X-Pagination");
            });
        });
        return services;
    }

    public static WebApplication UseCore(this WebApplication app)
        => app
            .UseDisciplineCors();
    
    private static WebApplication UseDisciplineCors(this WebApplication app)
    {
        app.UseCors(CorsName);
        return app;
    }

    internal static T GetOptions<T>(this IConfiguration configuration, string section) where T : class, new()
    {
        T t = new();
        configuration.Bind(section, t);
        return t;
    }
}