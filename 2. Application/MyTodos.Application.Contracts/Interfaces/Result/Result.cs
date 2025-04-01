using MyTodos.Core.Enums;

namespace MyTodos.Application.Contracts.Interfaces.Result;

public class Result<T> : Result, IResult<T>
{
    public T Data { get; set; }

    public static Result<T> Failed()
    {
        return new Result<T> { Succeeded = false };
    }

    public static Result<T> Failed(Message message)
    {
        return new Result<T> { Succeeded = false, Messages = [message] };
    }

    public static Result<T> Failed(string message, Reason reason)
    {
        return Failed(new Message(message, reason));
    }

    public static Result<T> Failed(ICollection<Message> messages)
    {
        return new Result<T> { Succeeded = false, Messages = messages };
    }

    public static Result<T> Failed(T data, ICollection<Message> messages)
    {
        return new Result<T> { Succeeded = false, Data = data, Messages = messages };
    }

    public static Task<Result<T>> FailedAsync()
    {
        return Task.FromResult(Failed());
    }

    public static Task<Result<T>> FailedAsync(Message message)
    {
        return Task.FromResult(Failed(message));
    }

    public static Task<Result<T>> FailedAsync(string message, Reason reason)
    {
        return Task.FromResult(Failed(message, reason));
    }

    public static Task<Result<T>> FailedAsync(ICollection<Message> messages)
    {
        return Task.FromResult(Failed(messages));
    }

    public static Task<Result<T>> FailedAsync(T data, ICollection<Message> messages)
    {
        return Task.FromResult(Failed(data, messages));
    }

    public static Result<T> Success()
    {
        return new Result<T> { Succeeded = true };
    }

    public static Result<T> Success(Message message)
    {
        return new Result<T> { Succeeded = true, Messages = [message] };
    }

    public static Result<T> Success(T data)
    {
        return new Result<T> { Succeeded = true, Data = data };
    }

    public static Result<T> Success(T data, Message message)
    {
        return new Result<T> { Succeeded = true, Data = data, Messages = [message] };
    }

    public static Result<T> Success(T data, ICollection<Message> messages)
    {
        return new Result<T> { Succeeded = true, Data = data, Messages = messages };
    }

    public new static Task<Result<T>> SuccessAsync(Message message)
    {
        return Task.FromResult(Success(message));
    }

    public static Task<Result<T>> SuccessAsync(T data)
    {
        return Task.FromResult(Success(data));
    }

    public static Task<Result<T>> SuccessAsync(T data, Message message)
    {
        return Task.FromResult(Success(data, message));
    }

    public static Task<Result<T>> SuccessAsync(T data, ICollection<Message> messages)
    {
        return Task.FromResult(Success(data, messages));
    }
}

public class Result : IResult
{
    public bool Succeeded { get; set; }
    public ICollection<Message> Messages { get; set; } = new List<Message>();

    public static Result Failed()
    {
        return new Result { Succeeded = false };
    }

    public static Result Failed(Message message)
    {
        return new Result { Succeeded = false, Messages = [message] };
    }

    public static Result Failed(string message, Reason reason)
    {
        return Failed(new Message(message, reason));
    }

    public static Result Failed(ICollection<Message> messages)
    {
        return new Result { Succeeded = false, Messages = messages };
    }

    public static Task<Result> FailedAsync()
    {
        return Task.FromResult(Failed());
    }

    public static Task<Result> FailedAsync(Message message)
    {
        return Task.FromResult(Failed(message));
    }

    public static Task<Result> FailedAsync(string message, Reason reason)
    {
        return Task.FromResult(Failed(message, reason));
    }

    public static Task<Result> FailedAsync(ICollection<Message> messages)
    {
        return Task.FromResult(Failed(messages));
    }

    public static Result Success()
    {
        return new Result { Succeeded = true };
    }

    public static Result Success(Message message)
    {
        return new Result { Succeeded = true, Messages = [message] };
    }

    public static Task<Result> SuccessAsync()
    {
        return Task.FromResult(Success());
    }

    public static Task<Result> SuccessAsync(Message message)
    {
        return Task.FromResult(Success(message));
    }
}