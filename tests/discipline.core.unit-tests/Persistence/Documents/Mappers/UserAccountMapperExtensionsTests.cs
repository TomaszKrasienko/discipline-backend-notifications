using discipline.core.Persistence.Documents.Mappers;
using discipline.tests.shared.Entities;
using Shouldly;
using Xunit;

namespace discipline.core.unit_tests.Persistence.Documents.Mappers;

public sealed class UserAccountMapperExtensionsTests
{
    [Fact]
    public void AsDocument_GivenUserAccountWithoutNotifications_ShouldReturnUserAccountDocument()
    {
        //arrange
        var userAccount = UserAccountFactory.Get();
        
        //act
        var result = userAccount.AsDocument();
        
        //assert
        result.UserId.ShouldBe(userAccount.UserId);
        result.Notifications.ShouldBeNull();
    }

    [Fact]
    public void AsDocument_GivenUserAccountWithNotifications_ShouldReturnUserAccountDocumentWithNotificationDocuments()
    {
        //arrange
        var userAccount = UserAccountFactory.Get(true);
        
        //act
        var result = userAccount.AsDocument();
        
        //assert
        result.UserId.ShouldBe(userAccount.UserId);
        result.Notifications.First().NotificationId.ShouldBe(userAccount.Notifications.First().NotificationId);
        result.Notifications.First().Title.ShouldBe(userAccount.Notifications.First().Title);
        result.Notifications.First().Content.ShouldBe(userAccount.Notifications.First().Content);
        result.Notifications.First().CreatedAt.ShouldBe(userAccount.Notifications.First().CreatedAt);
        result.Notifications.First().IsRead.ShouldBe(userAccount.Notifications.First().IsRead);
        result.Notifications.Count().ShouldBe(userAccount.Notifications.Count);
    }
}