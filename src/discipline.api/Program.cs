using discipline.core.Communication.SignalR;
using discipline.core.Configuration;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore();

var app = builder.Build();

app.MapPost("/", (IHubContext<NotificationHub> context) =>
{
    context.Clients.All.SendAsync("test", "test");
});
app.MapHub<NotificationHub>("/discipline-notifications-hub");
app.UseHttpsRedirection();
app.Run();