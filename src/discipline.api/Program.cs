using discipline.core.Communication;
using discipline.core.Communication.SignalR;
using discipline.core.Configuration;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCore(builder.Configuration);

var app = builder.Build();
app.UseCore();
app.MapPost("/send-notification",async (INotificationWrapper wrapper) =>
{
    await wrapper.Send(new { Test = "Test" });
});
app.UseHttpsRedirection();
app.Run();