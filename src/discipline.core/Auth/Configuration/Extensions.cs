using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using discipline.core.Communication.Notifications.SignalR.Configuration;
using discipline.core.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace discipline.core.Auth.Configuration;

internal static class Extensions
{
    private const string SectionName = "Auth";
    internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetOptions<AuthOptions>(SectionName);
        var validationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = authOptions.Issuer,
            ValidAudience = authOptions.Audience,
            LogValidationExceptions = true,
        };
        
        RSA publicRsa = RSA.Create();
        publicRsa.ImportFromPem(File.ReadAllText(authOptions.PublicCertPath));
        var publicKey = new RsaSecurityKey(publicRsa);
        validationParameters.IssuerSigningKey = publicKey;
        services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = validationParameters;
                options.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        try
                        {
                            var path = context.HttpContext.Request.Path;

                            if (!context.Request.Headers.TryGetValue("Authorization", out var accessToken))
                            {
                                Results.Unauthorized();
                            }
                            accessToken = accessToken.First().Replace("Bearer", "").Trim();
                            
                            bool IsValidPath()
                            {
                                var signalROptions = configuration.GetOptions<Dictionary<string, SignalROptions>>(
                                        Communication.Notifications.SignalR.Configuration.Extensions.SectionName);
                                return signalROptions.Any(option 
                                    => path.StartsWithSegments(option.Value.Route));
                            }

                            if (!string.IsNullOrEmpty(accessToken) && IsValidPath())
                            {
                                context.Token = accessToken;
                            }

                        }
                        catch (Exception ex)
                        {
                            Results.Unauthorized();
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        
        return services;
    }

    internal static WebApplication UseAuth(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}