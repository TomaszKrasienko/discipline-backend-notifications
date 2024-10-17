using discipline.core.Exceptions;

namespace discipline.core.Domain.NotificationDefinitions.Exceptions;

public sealed class EmptyContextException()
    : DisciplineException("Context can not be empty");