using discipline.core.Domain.SharedKernel.Exceptions;

namespace discipline.core.Domain.SharedKernel.Types;

public sealed class EntityId : ValueObject
{
    public Guid Value { get; }

    public EntityId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new EmptyEntityIdException();
        }
        Value = value;
    }

    public static implicit operator Guid(EntityId entityId)
        => entityId.Value;

    public static implicit operator EntityId(Guid value)
        => new EntityId(value);
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}