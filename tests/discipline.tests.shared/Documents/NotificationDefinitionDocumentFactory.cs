using Bogus;
using discipline.core.Persistence.Documents;

namespace discipline.tests.shared.Documents;

internal static class NotificationDefinitionDocumentFactory
{
    internal static NotificationDefinitionDocument Get()
        => Get(1).Single();

    internal static List<NotificationDefinitionDocument> Get(int count)
        => GetFaker().Generate(count);

    private static Faker<NotificationDefinitionDocument> GetFaker()
        => new Faker<NotificationDefinitionDocument>()
            .RuleFor(f => f.Id, v => Guid.NewGuid())
            .RuleFor(f => f.Context, v => v.Lorem.Word())
            .RuleFor(f => f.Title, v => v.Lorem.Word())
            .RuleFor(f => f.Content, v => v.Lorem.Sentence());
}