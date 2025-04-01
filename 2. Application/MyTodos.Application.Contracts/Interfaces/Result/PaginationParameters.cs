namespace MyTodos.Application.Contracts.Interfaces.Result;

public class PaginationParameters
{
    public PaginationParameters()
    {
    }

    public PaginationParameters(int? pageSize, int pageNumber)
    {
        if (pageSize != null && pageSize != 0) PageSize = pageSize.Value;
        PageNumber = pageNumber;
    }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; init; } = 10;
}