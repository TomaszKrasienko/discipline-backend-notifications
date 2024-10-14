using discipline.core.Domain.NotificationDefinitions.Entities;
using discipline.core.Domain.NotificationDefinitions.Exceptions;
using Shouldly;
using Xunit;

namespace discipline.core.unit_tests.Domain.NotificationsStorage.Entities;

public sealed class NotificationStorageTests
{
    [Fact]
    public void FillContent_GivenNullParameters_ShouldReturnContentWithoutChanges()
    {
        //arrange
        var notificationDefinition = NotificationDefinition.Create(Guid.NewGuid(),
            "test_context", "test_title", "test_content");
        
        //act
        var result = notificationDefinition.FillContent(null);
        
        //assert
        result.ShouldBe(notificationDefinition.Content.Value);
    }
    
    [Fact]
    public void FillContent_GivenEmptyParameters_ShouldReturnContentWithoutChanges()
    {
        //arrange
        var notificationDefinition = NotificationDefinition.Create(Guid.NewGuid(),
            "test_context", "test_title", "test_content");
        
        //act
        var result = notificationDefinition.FillContent([]);
        
        //assert
        result.ShouldBe(notificationDefinition.Content.Value);
    }
    
    [Fact]
    public void FillContent_GivenInvalidParametersNumber_ShouldThrowInvalidNumberOfParametersException()
    {
        //arrange
        var notificationDefinition = NotificationDefinition.Create(Guid.NewGuid(),
            "test_context", "test_title", "Test {0} test {1}");
        
        //act
        var exception = Record.Exception(() => notificationDefinition.FillContent(["param1"]));
        
        //assert
        exception.ShouldBeOfType<InvalidNumberOfParametersException>();
    }
    
    [Theory]
    [MemberData(nameof(GetFillContentTestData))]
    public void FillContent_GivenParameters_ShouldReturnFilledContent(FillContentTestDate fillContentTestDate)
    {
        //arrange
        var notificationDefinition = NotificationDefinition.Create(Guid.NewGuid(),
            "test_context", "test_title", fillContentTestDate.Content);
        
        //act
        var result = notificationDefinition.FillContent(fillContentTestDate.Params);
        
        //assert
        result.ShouldBe(fillContentTestDate.FilledContent);
    }

    public static IEnumerable<object[]> GetFillContentTestData()
    {
        yield return new object[]
        {
            new FillContentTestDate()
            {
                Content = "Test {0} test {1}",
                Params = ["param1", "param2"],
                FilledContent = "Test param1 test param2"
            }
        };

        yield return new object[]
        {
            new FillContentTestDate()
            {
                Content = "Test {0}",
                Params = ["param1"],
                FilledContent = "Test param1"
            }
        };
    }
}

public class FillContentTestDate
{
    public string Content { get; set; }
    public List<string> Params { get; set; }
    public string FilledContent { get; set; }
}