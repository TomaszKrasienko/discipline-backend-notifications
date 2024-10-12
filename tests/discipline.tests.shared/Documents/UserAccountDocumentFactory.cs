using Bogus;
using discipline.core.Persistence.Documents;

namespace discipline.tests.shared.Documents;

internal static class UserAccountDocumentFactory
{
    internal static UserAccountDocument Get()
        => Get(1).Single();
    
    internal static List<UserAccountDocument> Get(int count)
        => GetFaker().Generate(count);
    
    private static Faker<UserAccountDocument> GetFaker()
        => new Faker<UserAccountDocument>()
            .RuleFor(f => f.UserId, v => Guid.NewGuid());
}