using MyTodos.Core.Enums;

namespace MyTodos.Application.Contracts.Interfaces.Result;

public class Message
{
    public Message()
    {
        Timestamp = DateTimeOffset.UtcNow;
    }

    public Message(string? description, Reason reason = Reason.None, string? field = null)
    {
        Description = description;
        Reason = reason;
        Field = field;
        Timestamp = DateTimeOffset.UtcNow;
    }

    public string? Field { get; init; }
    public string? Description { get; init; }
    public Reason Reason { get; init; }
    public DateTimeOffset? Timestamp { get; init; }
}