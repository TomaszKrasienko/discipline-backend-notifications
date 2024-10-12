using Bogus;
using discipline.core.Entities;
using discipline.core.Persistence.Documents;

namespace discipline.tests.shared.Documents;

internal static class NotificationDocumentFactory
{
    internal static Faker<NotificationDocument> GetFaker()
        => new Faker<NotificationDocument>()
            .RuleFor(f => f.NotificationId, v => Guid.NewGuid())
            .RuleFor(f => f.Title, v => v.Lorem.Word())
            .RuleFor(f => f.Content, v => v.Lorem.Sentence())
            .RuleFor(f => f.CreatedAt, v => DateTime.Now)
            .RuleFor(f => f.IsRead, v => v.Random.Bool())

}