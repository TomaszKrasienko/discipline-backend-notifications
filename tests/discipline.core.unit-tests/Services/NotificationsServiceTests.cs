using System.Globalization;
using Amazon.Runtime;
using discipline.core.Communication.Notifications;
using discipline.core.Domain.NotificationDefinitions.Entities;
using discipline.core.Domain.NotificationDefinitions.Repositories;
using discipline.core.DTOs;
using discipline.core.Persistence.Repositories.Abstractions;
using discipline.core.Services.Abstractions;
using discipline.core.Services.Commands;
using discipline.core.Services.Internals;
using discipline.core.Time.Abstractions;
using discipline.tests.shared.Entities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Xunit;

namespace discipline.core.unit_tests.Services;

public sealed class NotificationsServiceTests
{
    [Fact]
    public async Task SendNotificationAsync_GivenExistingUserAccountAndExistingNotificationDefinition_ShouldAddNotificationAndSendByWrapper()
    {
        //arrange
        var userAccount = UserAccountFactory.Get();
        var notificationDefinition = NotificationDefinition.Create(Guid.NewGuid(),
            "test_context", "test_title", "my_content_{0}");
        var command = new NewNotificationCommand(userAccount.UserId, notificationDefinition.Context,
            ["my_test_parameter"]);

        var message = string.Format(notificationDefinition.Content.Value, command.Parameters.ToArray<object>());
        var createdAt = DateTime.UtcNow;
        
        _userAccountRepository
            .GetByIdAsync(userAccount.UserId, default)
            .Returns(userAccount);

        _notificationDefinitionRepository
            .GetByIdAsync(notificationDefinition.Context, default)
            .Returns(notificationDefinition);

        _clock
            .Now()
            .Returns(createdAt);
        
        //act
        await _service.SendNotificationAsync(command, default);
        
        //assert
        userAccount
            .Notifications
            .Any(x => x.CreatedAt == createdAt)
            .ShouldBeTrue();

        await _userAccountRepository
            .Received(1)
            .UpdateAsync(userAccount, default);

        await _notificationWrapper
            .Received(1)
            .SendForUser(userAccount.UserId, Arg.Is<NotificationDto>(arg
                => arg.Title == notificationDefinition.Title
                && arg.Content == message));
    }
    
    [Fact]
    public async Task SendNotificationAsync_GivenNotExistingUser_ShouldNotSendAnyNotification()
    {
        //arrange
        var command = new NewNotificationCommand(Guid.NewGuid(), "test_context", null);

        _userAccountRepository
            .GetByIdAsync(command.UserId, default)
            .ReturnsNull();
        
        //act
        await _service.SendNotificationAsync(command, default);
        
        //assert
        _notificationWrapper
            .Received(0)
            .CanByApplied(Arg.Any<NotificationType>());

        await _notificationWrapper
            .Received(0)
            .SendForUser(Arg.Any<Guid>(), Arg.Any<object>());
    }
    
    [Fact]
    public async Task SendSystemNotification_GivenNotExistingNotificationDefinition_ShouldNotSendAnyNotification()
    {
        //arrange
        var userAccount = UserAccountFactory.Get();
        
        var command = new NewNotificationCommand(userAccount.UserId, "test_context", null);

        _userAccountRepository
            .GetByIdAsync(command.UserId, default)
            .Returns(userAccount);

        _notificationDefinitionRepository
            .GetByIdAsync(command.Context, default)
            .ReturnsNull();
        
        //act
        await _service.SendNotificationAsync(command, default);
        
        //assert
        _notificationWrapper
            .Received(0)
            .CanByApplied(Arg.Any<NotificationType>());

        await _notificationWrapper
            .Received(0)
            .SendForUser(Arg.Any<Guid>(), Arg.Any<object>());
    }
    
    #region arrange

    private readonly ILogger<NotificationsService> _logger;
    private readonly IUserAccountRepository _userAccountRepository;
    private readonly INotificationDefinitionRepository _notificationDefinitionRepository;
    private readonly IClock _clock;
    private readonly INotificationWrapper _notificationWrapper;
    private readonly INotificationsService _service;

    public NotificationsServiceTests()
    {
        _logger = Substitute.For<ILogger<NotificationsService>>();
        _userAccountRepository = Substitute.For<IUserAccountRepository>();
        _notificationDefinitionRepository = Substitute.For<INotificationDefinitionRepository>();
        _clock = Substitute.For<IClock>();
        _notificationWrapper = Substitute.For<INotificationWrapper>();
        _service = new NotificationsService(_logger, _userAccountRepository, 
            _notificationDefinitionRepository, _clock, _notificationWrapper);
    }
    #endregion
}