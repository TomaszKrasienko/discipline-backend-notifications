using discipline.core.Time.Abstractions;

namespace discipline.core.Time.Internals;

internal sealed class Clock : IClock
{
    public DateTime Now()
        => DateTime.Now;
}