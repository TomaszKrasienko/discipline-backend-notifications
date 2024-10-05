using discipline.core.Communication.SignalR.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace discipline.core.Configuration;

public static class Extensions
{
    private const string CorsName = "discipline-notifications-cors";
    
    public static IServiceCollection AddCore(this IServiceCollection services)
        => services
            .AddBroadcasting();

    private static IServiceCollection AddDisciplineCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("test", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("X-Pagination");
            });
        });
        return services;
    }

    // private static WebApplication UseDisciplineCors(this WebApplication app)
    // {
    //     
    // }
}