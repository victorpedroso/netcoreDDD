namespace TechSolutions.Application.Models;

public class Result<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }

    private Result(bool success, string? message, T? data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static Result<T> IsSuccess(T data) => new(true, null, data);
    public static Result<T> IsError(string message) => new(false, message, default);
}
