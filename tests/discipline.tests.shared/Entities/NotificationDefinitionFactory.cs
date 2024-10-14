using Bogus;
using discipline.core.Domain.NotificationDefinitions.Entities;

namespace discipline.tests.shared.Entities;

internal static class NotificationDefinitionFactory
{
    internal static NotificationDefinition Get()
        => Get(1).Single();

    internal static List<NotificationDefinition> Get(int count)
        => GetFaker().Generate(count);
    
    private static Faker<NotificationDefinition> GetFaker()
        => new Faker<NotificationDefinition>().CustomInstantiator(v =>
            new NotificationDefinition(Guid.NewGuid(), v.Lorem.Word(),
                v.Lorem.Word(), v.Lorem.Sentence()));
}