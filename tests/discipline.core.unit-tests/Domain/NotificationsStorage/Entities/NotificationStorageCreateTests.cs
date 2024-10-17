using discipline.core.Domain.NotificationDefinitions.Entities;
using discipline.core.Domain.NotificationDefinitions.Exceptions;
using discipline.core.Domain.SharedKernel.Exceptions;
using Shouldly;
using Xunit;

namespace discipline.core.unit_tests.Domain.NotificationsStorage.Entities;

public sealed class NotificationStorageCreateTests
{
    [Theory]
    [InlineData("test {0} test", 1)]
    [InlineData("test {0} test {1}", 2)]
    [InlineData("test test", 0)]
    public void Create_GivenValidArguments_ShouldReturnNotificationStorage(string content, int paramsCount)
    {
        //arrange
        var id = Guid.NewGuid();
        var title = "test_title";
        var context = "test_context";
        
        //act
        var result = NotificationDefinition.Create(id, context, title, content);
        
        //assert
        result.Id.Value.ShouldBe(id);
        result?.Context?.Value.ShouldBe(context);
        result?.Title?.Value.ShouldBe(title);
        result?.Content?.Value.ShouldBe(content);
        result?.Content?.ParamCount.ShouldBe(paramsCount);
    }

    [Fact]
    public void Create_GivenEmptyId_ShouldThrowEmptyEntityIdException()
    {
        //act
        var exception = Record.Exception(() => NotificationDefinition.Create(Guid.Empty, "context",
            "title", "content"));
        
        //assert
        exception.ShouldBeOfType<EmptyEntityIdException>();
    }
    
    [Fact]
    public void Create_GivenEmptyContext_ShouldThrowEmptyContextException()
    {
        //act
        var exception = Record.Exception(() => NotificationDefinition.Create(Guid.NewGuid(), string.Empty,
            "title", "content"));
        
        //assert
        exception.ShouldBeOfType<EmptyContextException>();
    }
    
    [Fact]
    public void Create_GivenEmptyTitle_ShouldThrowEmptyTitleException()
    {
        //act
        var exception = Record.Exception(() => NotificationDefinition.Create(Guid.NewGuid(), "context",
            string.Empty, "content"));
        
        //assert
        exception.ShouldBeOfType<EmptyTitleException>();
    }
    
    [Fact]
    public void Create_GivenEmptyContent_ShouldThrowEmptyContentException()
    {
        //act
        var exception = Record.Exception(() => NotificationDefinition.Create(Guid.NewGuid(), "context",
            "title", string.Empty));
        
        //assert
        exception.ShouldBeOfType<EmptyContentException>();
    }
}