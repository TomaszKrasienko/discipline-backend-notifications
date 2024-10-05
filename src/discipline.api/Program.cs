using discipline.core.Communication.SignalR;
using discipline.core.Configuration;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore();
builder.Services.AddCors(options =>
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
var app = builder.Build();
app.UseCors("test");
app.MapPost("/", (IHubContext<NotificationHub> context) =>
{
    context.Clients.All.SendAsync("test", "test");
});
app.MapHub<NotificationHub>("/discipline-notifications-hub");
app.UseHttpsRedirection();
app.Run();