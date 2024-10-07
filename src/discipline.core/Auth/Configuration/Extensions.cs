using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using discipline.core.Communication.SignalR.Configuration;
using discipline.core.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
                        var path = context.HttpContext.Request.Path;
                        var headers = context.Request.Headers.TryGetValue("Authorization", out var accessToken);
                        var signalROptions = configuration.GetOptions<Dictionary<string, SignalROptions>>(Communication.SignalR.Configuration.Extensions.SectionName);
                        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                        var result = handler.ValidateToken(accessToken.First(), validationParameters, out SecurityToken validatedToken);
                        var token = handler.ReadJwtToken(accessToken.First());
                        if (!string.IsNullOrEmpty(accessToken) && IsValidPath() && (result?.Identity?.IsAuthenticated ?? false))
                        {
                            context.Token = accessToken.First();
                        }
                        
                        return Task.CompletedTask;

                        bool IsValidPath() => 
                            signalROptions.Any(option => path.StartsWithSegments(option.Value.Route));
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