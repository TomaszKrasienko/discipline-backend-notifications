using discipline.core.Persistence.Documents;
using discipline.core.Persistence.Documents.Mappers;
using discipline.tests.shared.Documents;
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
        result.Notifications.ShouldBeEmpty();
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

    [Fact]
    public void AsEntity_GivenUserAccountWithoutNotifications_ShouldReturnUserAccount()
    {
        //arrange
        var userAccountDocument = UserAccountDocumentFactory.Get();
        
        //act
        var result = userAccountDocument.AsEntity();
        
        //assert
        result.UserId.ShouldBe(userAccountDocument.UserId);
        result.Notifications.ShouldBeEmpty();
    }

    [Fact]
    public void AsEntity_GivenUserAccountWithNotifications_ShouldReturnUserAccountWithNotifications()
    {
        //arrange
        var userAccountDocument = UserAccountDocumentFactory.Get();
        var notificationDocuments = NotificationDocumentFactory.Get(5);
        userAccountDocument.Notifications = notificationDocuments;
        
        //act
        var result = userAccountDocument.AsEntity();
        
        //assert
        result.UserId.ShouldBe(userAccountDocument.UserId);
        result.Notifications.Count().ShouldBe(notificationDocuments.Count);
        result.Notifications.First().NotificationId.ShouldBe(notificationDocuments[0].NotificationId);
        result.Notifications.First().Title.ShouldBe(notificationDocuments[0].Title);
        result.Notifications.First().Content.ShouldBe(notificationDocuments[0].Content);
        result.Notifications.First().CreatedAt.ShouldBe(notificationDocuments[0].CreatedAt);
        result.Notifications.First().IsRead.ShouldBe(notificationDocuments[0].IsRead);
    }
}