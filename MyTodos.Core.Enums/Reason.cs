namespace MyTodos.Core.Enums;

public enum Reason
{
    None = 0,
    AlreadyExist = 1,
    NotFound = 2,
    NotValid = 3,
    Unauthorized = 4,
    WrongCode = 5,
    Dependency = 6,
    Unhandled = 7
}