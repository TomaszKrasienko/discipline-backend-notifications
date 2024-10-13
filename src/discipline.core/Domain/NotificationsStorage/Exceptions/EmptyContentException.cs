using discipline.core.Exceptions;

namespace discipline.core.Domain.NotificationsStorage.Exceptions;

public sealed class EmptyContentException()
    : DisciplineException("Content can not be empty");