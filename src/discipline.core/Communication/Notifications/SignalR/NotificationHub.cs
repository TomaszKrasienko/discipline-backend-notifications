using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace discipline.core.Communication.Notifications.SignalR;

[Authorize]
public sealed class NotificationHub
    (ILogger<NotificationHub> logger) : Hub
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
    
    public override Task OnConnectedAsync()
    {
        logger.LogInformation($"User {Context?.User?.Identity?.Name} connected");
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        logger.LogInformation($"User disconnected");
        return base.OnDisconnectedAsync(exception);
    }
}