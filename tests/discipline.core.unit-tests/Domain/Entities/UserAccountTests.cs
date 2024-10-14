using discipline.core.Domain.UserAccounts.Entities;
using discipline.core.Exceptions;
using Shouldly;
using Xunit;

namespace discipline.core.unit_tests.Domain.Entities;

public sealed class UserAccountTests
{
    [Fact]
    public void Create_GivenEmptyId_ShouldThrowEmptyUserIdException()
    {
        //act
        var exception = Record.Exception(() => UserAccount.Create(Guid.Empty));
        
        //assert
        exception.ShouldBeOfType<EmptyUserIdException>();
    }

    [Fact]
    public void Create_GivenUserId_ShouldReturnUserWithUserIdAndNotNullNotification()
    {
        //arrange
        var id = Guid.NewGuid();
        
        //act
        var result = UserAccount.Create(id);
        
        //assert
        result.ShouldNotBeNull();
        result.UserId.ShouldBe(id);
        result.Notifications.ShouldNotBeNull();
    }

    [Fact]
    public void AddNotification_GivenArguments_ShouldAddToNotificationsAndReturnId()
    {
        //arrange
        var userAccount = UserAccount.Create(Guid.NewGuid());
        var title = "test_title";
        var content = "test_content";
        var createdAt = DateTime.Now;

        //act
        var id = userAccount.AddNotification(title, content, createdAt);
        
        //assert
        id.ShouldNotBe(Guid.Empty);
        var newNotification = userAccount.Notifications.First(x => x.NotificationId == id);
        newNotification.Title.ShouldBe(title);
        newNotification.Content.ShouldBe(content);
        newNotification.CreatedAt.ShouldBe(createdAt);
    }
}