namespace KDramaSystem.Application.Communication;

public class Result<T>
{
    public bool Success { get; }
    public T? Data { get; }
    public Error? Error { get; }

    protected Result(T data)
    {
        Success = true;
        Data = data;
    }

    protected Result(Error error)
    {
        Success = false;
        Error = error;
    }

    public static Result<T> Ok(T data) => new(data);

    public static Result<T> Fail(Error error) => new(error);
}