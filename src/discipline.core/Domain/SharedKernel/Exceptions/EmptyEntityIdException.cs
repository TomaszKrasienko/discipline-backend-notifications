using discipline.core.Exceptions;

namespace discipline.core.Domain.SharedKernel.Exceptions;

public sealed class EmptyEntityIdException()
    : DisciplineException("Entity Id can not be empty");