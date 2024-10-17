using discipline.core.Exceptions;

namespace discipline.core.Domain.NotificationDefinitions.Exceptions;

public sealed class EmptyContentException()
    : DisciplineException("Content can not be empty");