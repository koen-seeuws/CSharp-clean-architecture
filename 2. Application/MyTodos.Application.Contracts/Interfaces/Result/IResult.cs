namespace MyTodos.Application.Contracts.Interfaces.Result;

public interface IResult
{
    ICollection<Message> Messages { get; set; }

    bool Succeeded { get; set; }
}

public interface IResult<out T> : IResult
{
    T Data { get; }
}