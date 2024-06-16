namespace API.Domain.Common;

public record Error(string? Title = null, int? ErrorCode = null, bool IsValidationError = false);