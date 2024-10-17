using Bogus;
using discipline.core.Domain.UserAccounts.Entities;

namespace discipline.tests.shared.Entities;

internal static class UserAccountFactory
{
    internal static UserAccount Get(bool withNotification = false)
        => Get(1, withNotification).Single();
    
    internal static List<UserAccount> Get(int count, bool withNotification = false)
    {
        Faker<UserAccount> faker;
        if(withNotification)
        {
            var notificationsCount = new Random().Next(1, 10);
            var notifications = NotificationFactory.Get(notificationsCount);
            faker = GetFaker(notifications);
        }
        else
        {
            faker = GetFaker();
        }

        return faker.Generate(count);
    }

    private static Faker<UserAccount> GetFaker(List<Notification>? notifications = null)
        => new Faker<UserAccount>()
            .CustomInstantiator(f => new UserAccount(
                Guid.NewGuid(), notifications));
}