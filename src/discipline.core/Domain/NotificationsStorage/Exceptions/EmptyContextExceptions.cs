using discipline.core.Exceptions;

namespace discipline.core.Domain.NotificationsStorage.Exceptions;

public sealed class EmptyContextException()
    : DisciplineException("Context can not be empty");