using Store.Domain.Commands.Interfaces;

namespace Store.Domain.Commands;

public class GenericsCommandResult<T>(bool success, string message, T data) : ICommandResult
{
    public bool Success { get; set; } = success;
    public string Message { get; set; } = message;
    public T Data { get; set; } = data;
}