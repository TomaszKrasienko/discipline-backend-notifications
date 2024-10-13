using discipline.core.Exceptions;

namespace discipline.core.Domain.NotificationsStorage.Exceptions;

public sealed class EmptyTitleException()
    : DisciplineException("Title can not be empty");