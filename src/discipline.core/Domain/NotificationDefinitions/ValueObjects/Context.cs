using discipline.core.Domain.NotificationDefinitions.Exceptions;
using discipline.core.Domain.SharedKernel;

namespace discipline.core.Domain.NotificationDefinitions.ValueObjects;

public sealed class Context : ValueObject
{
    public string Value { get; }

    public Context(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyContextException();
        }
        Value = value;
    }

    public static implicit operator string(Context context)
        => context.Value;

    public static implicit operator Context(string value)
        => new Context(value);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}