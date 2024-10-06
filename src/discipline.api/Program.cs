using discipline.core.Communication.SignalR;
using discipline.core.Configuration;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore();

var app = builder.Build();
app.UseCore();
app.MapHub<NotificationHub>("/discipline-notifications-hub");
app.MapPost("/send-notification",async (IHubContext<NotificationHub> context) =>
{
    
    await context.Clients.All.SendAsync("user-notifications", "test");
});
app.UseHttpsRedirection();
app.Run();