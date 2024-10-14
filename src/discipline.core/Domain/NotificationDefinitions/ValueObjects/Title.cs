using discipline.core.Domain.NotificationDefinitions.Exceptions;
using discipline.core.Domain.SharedKernel;

namespace discipline.core.Domain.NotificationDefinitions.ValueObjects;

public sealed class Title : ValueObject
{
    public string Value { get; }

    public Title(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyTitleException();
        }
        Value = value;
    }
    
    public static implicit operator string(Title context)
        => context.Value;

    public static implicit operator Title(string value)
        => new Title(value);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}