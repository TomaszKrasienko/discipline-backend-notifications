using discipline.core.Exceptions;

namespace discipline.core.Domain.NotificationDefinitions.Exceptions;

public sealed class InvalidNumberOfParametersException(int providedParametersCount, int definitionParametersCount)
    : DisciplineException($"Invalid parameters count. Provided parameters count: {providedParametersCount}. Definition parameters count: {definitionParametersCount}");