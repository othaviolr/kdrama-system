namespace KDramaSystem.Application.Communication;

public class Error
{
    public string Code { get; }
    public string Message { get; }
    public ErrorType Type { get; }

    public Error(string code, string message, ErrorType type)
    {
        Code = code;
        Message = message;
        Type = type;
    }

    public static Error NotFound(string message) =>
        new("NOT_FOUND", message, ErrorType.NotFound);

    public static Error Validation(string message) =>
        new("VALIDATION_ERROR", message, ErrorType.Validation);

    public static Error Conflict(string message) =>
        new("CONFLICT", message, ErrorType.Conflict);

    public static Error Unexpected(string message) =>
        new("UNEXPECTED", message, ErrorType.Unexpected);
}