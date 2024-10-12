using discipline.core.Communication.Notifications;
using discipline.core.Persistence.Repositories.Abstractions;
using discipline.core.Services.Abstractions;
using discipline.core.Services.Commands;
using discipline.core.Services.Internals;
using discipline.core.Time.Abstractions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace discipline.core.unit_tests.Services;

public sealed class NotificationsServiceTests
{

    [Fact]
    public async Task SendSystemNotification_GivenNotExistingUser_ShouldNotSendAnyNotification()
    {
        //arrange
        var command = new NewSystemNotification(Guid.NewGuid(), "test_context");

        _userAccountRepository
            .GetByIdAsync(command.UserId, default)
            .ReturnsNull();
        
        //act
        await _service.SendSystemNotification(command, default);
        
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
    private readonly IClock _clock;
    private readonly INotificationWrapper _notificationWrapper;
    private readonly INotificationsService _service;

    public NotificationsServiceTests()
    {
        _logger = Substitute.For<ILogger<NotificationsService>>();
        _userAccountRepository = Substitute.For<IUserAccountRepository>();
        _clock = Substitute.For<IClock>();
        _notificationWrapper = Substitute.For<INotificationWrapper>();
        _service = new NotificationsService(_logger, _userAccountRepository, _clock, _notificationWrapper);
    }
    #endregion
}