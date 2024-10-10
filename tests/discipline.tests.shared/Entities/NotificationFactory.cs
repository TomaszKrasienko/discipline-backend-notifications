using Bogus;
using discipline.core.Entities;

namespace discipline.tests.shared.Entities;

internal static class NotificationFactory
{
    internal static Notification Get()
        => Get(1).Single();
    
    internal static List<Notification> Get(int count)
        => GetFaker().Generate(count);

    private static Faker<Notification> GetFaker()
        => new Faker<Notification>()
            .CustomInstantiator(f => new Notification(
                Guid.NewGuid(),
                f.Lorem.Word(),
                f.Lorem.Sentence(),
                DateTime.Now,
                f.Random.Bool()));
}