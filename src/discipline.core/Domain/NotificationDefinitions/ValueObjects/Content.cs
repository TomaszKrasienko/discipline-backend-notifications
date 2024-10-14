using System.Text.RegularExpressions;
using discipline.core.Domain.NotificationDefinitions.Exceptions;
using discipline.core.Domain.SharedKernel;

namespace discipline.core.Domain.NotificationDefinitions.ValueObjects;

public sealed class Content : ValueObject
{
    public string Value { get; }
    public int ParamCount { get; private set; }
    
    public Content(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyContentException();
        }
        Value = value;
        SetParamCount(value);
    }

    private void SetParamCount(string value)
    {
        var matches = Regex.Matches(value, @"\{\d+\}");
        ParamCount = matches.Count;
    }

    public static implicit operator Content(string value)
        => new Content(value);
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return ParamCount;
    }
}