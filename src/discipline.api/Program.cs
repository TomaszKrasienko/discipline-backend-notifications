using discipline.core.Communication;
using discipline.core.Communication.Notifications;
using discipline.core.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore(builder.Configuration);

var app = builder.Build();
app.UseCore();
app.MapPost("/send-notification",async (INotificationWrapper wrapper) =>
{
    await wrapper.SendForAll(new { Test = "Test" });
});

app.MapPost("/send-notification/{userId:guid}",async (Guid userId,
    INotificationWrapper wrapper) =>
{
    await wrapper.SendForUser(userId,new { Test = "Test" });
});

app.UseHttpsRedirection();
app.Run();