using discipline.core.Domain.NotificationsStorage.Exceptions;
using discipline.core.Domain.SharedKernel;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace discipline.core.Domain.NotificationsStorage.ValueObjects;

public sealed class Content : ValueObject
{
    public string Value { get; }
    public int ParamCount { get; }
    
    public Content(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyContentException();
        }
        Value = value;
        //TODO: Params
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
        yield return ParamCount;
    }
}