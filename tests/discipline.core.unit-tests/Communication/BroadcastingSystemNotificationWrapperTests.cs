using discipline.core.Communication;
using discipline.core.Communication.SignalR;
using discipline.core.Communication.SignalR.Registry;
using NSubstitute;
using Shouldly;
using Xunit;

namespace discipline.core.unit_tests.Communication;

public sealed class BroadcastingSystemNotificationWrapperTests
{
    [Theory]
    [InlineData(true, NotificationType.Chat)]
    [InlineData(false, NotificationType.Chat)]
    public void CanByApplied_GivenNotificationOtherThanSystem_ShouldReturnFalse(bool isKeyExists, NotificationType type)
    {
        //arrange
        _hubRegistry
            .IsKeyExists(type)
            .Returns(isKeyExists);

        //act
        var result = _notificationWrapper.CanByApplied(type);
        
        //assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void CanBeApplied_GivenNotExistingKey_ShouldReturnFalse()
    {
        //arrange
        _hubRegistry
            .IsKeyExists(Arg.Any<NotificationType>())
            .Returns(false);
        
        //act
        var result = _notificationWrapper.CanByApplied(NotificationType.System);
        
        //assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void CanBeApplied_GivenExistingKeyAndSystemNotificationType_ShouldReturnTrue()
    {
        //arrange
        _hubRegistry
            .IsKeyExists(Arg.Any<NotificationType>())
            .Returns(true);
        
        //act
        var result = _notificationWrapper.CanByApplied(NotificationType.System);
        
        //assert
        result.ShouldBeTrue();
    }
    
    #region arrange
    private readonly IHubRegistry _hubRegistry;
    private readonly IHubService _hubService;
    private readonly INotificationWrapper _notificationWrapper;

    public BroadcastingSystemNotificationWrapperTests()
    {
        _hubRegistry = Substitute.For<IHubRegistry>();
        _hubService = Substitute.For<IHubService>();
        _notificationWrapper = new BroadcastingSystemNotificationWrapper(
            _hubRegistry, _hubService);
    }
    #endregion
}