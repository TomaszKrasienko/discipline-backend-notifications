namespace discipline.core.Exceptions;

public sealed class ContextAlreadyRegisteredException(string context)
    : DisciplineException($"Context: {context} is already registered");