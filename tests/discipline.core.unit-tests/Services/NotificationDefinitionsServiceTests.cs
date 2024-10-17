using discipline.core.Domain.NotificationDefinitions.Entities;
using discipline.core.Domain.NotificationDefinitions.Repositories;
using discipline.core.Exceptions;
using discipline.core.Services.Abstractions;
using discipline.core.Services.Commands;
using discipline.core.Services.Internals;
using NSubstitute;
using Shouldly;
using Xunit;

namespace discipline.core.unit_tests.Services;

public sealed class NotificationDefinitionsServiceTests
{
    [Fact]
    public async Task AddNotificationDefinitionAsync_GivenNotExistingContext_ShouldAddNotificationDefinitionByRepository()
    {
        //arrange
        var command = new NewNotificationDefinitionCommand(Guid.NewGuid(), "context_test",
            "title_test", "content_test");
        _notificationDefinitionRepository
            .IsContextExistsAsync(command.Context, default)
            .Returns(false);
        
        //act
        await _service.AddNotificationDefinitionAsync(command, default);
        
        //assert
        await _notificationDefinitionRepository
            .Received(1)
            .AddAsync(Arg.Is<NotificationDefinition>(arg
                => arg.Id == command.Id
                   && arg.Context! == command.Context
                   && arg.Title! == command.Title
                   && arg.Content!.Value == command.Content), default);
    }
    
    [Fact]
    public async Task AddNotificationDefinitionAsync_GivenExistingContext_ShouldThrowContextAlreadyRegisteredException()
    {
        //arrange
        var command = new NewNotificationDefinitionCommand(Guid.NewGuid(), "context_test",
            "title_test", "content_test");
        _notificationDefinitionRepository
            .IsContextExistsAsync(command.Context, default)
            .Returns(true);
        
        //act
        var result = await Record.ExceptionAsync(async () 
            => await _service.AddNotificationDefinitionAsync(command, default));
        
        //assert
        result.ShouldBeOfType<ContextAlreadyRegisteredException>();
    }
    
    #region arrange
    private readonly INotificationDefinitionRepository _notificationDefinitionRepository;
    private readonly INotificationDefinitionsService _service;

    public NotificationDefinitionsServiceTests()
    {
        _notificationDefinitionRepository = Substitute.For<INotificationDefinitionRepository>();
        _service = new NotificationDefinitionsService(_notificationDefinitionRepository);
    }
    #endregion
}