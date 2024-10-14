using discipline.core.Exceptions;

namespace discipline.core.Domain.NotificationDefinitions.Exceptions;

public sealed class EmptyTitleException()
    : DisciplineException("Title can not be empty");