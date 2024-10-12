using Bogus;
using discipline.core.Persistence.Documents;

namespace discipline.tests.shared.Documents;

internal static class UserAccountDocumentFactory
{
    private static Faker<UserAccountDocument> GetFaker()
        => new Faker<UserAccountDocument>()
            .RuleFor(f => f.UserId, v => Guid.NewGuid());
}