namespace MyTodos.Application.Contracts.Result;

public class PaginatedResult<T> : Result
{
    public PaginatedResult(ICollection<T> data)
    {
        Data = data;
    }

    internal PaginatedResult(bool succeeded, ICollection<T> data = default!, int count = 0,
        int page = 1, int pageSize = 10, List<Message> messages = null!)
    {
        Data = data ?? new List<T>();
        CurrentPage = page;
        Succeeded = succeeded;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        TotalCount = count;
        Messages = messages ?? new List<Message>();
    }

    public ICollection<T> Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    public static PaginatedResult<T> Failure(List<Message> messages)
    {
        return new PaginatedResult<T>(false, messages: messages);
    }

    public static PaginatedResult<T> Success(List<T> data, int page, int pageSize)
    {
        var paginatedData = new List<T>();
        var start = (page - 1) * pageSize;
        var end = page * pageSize;
        for (var i = start; i < end; i++)
        {
            if (i == data.Count) break;
            paginatedData.Add(data[i]);
        }

        return new PaginatedResult<T>(true, paginatedData, data.Count, page, pageSize);
    }

    public static Task<PaginatedResult<T>> FailureAsync(List<Message> messages)
    {
        return Task.FromResult(Failure(messages));
    }

    public static Task<PaginatedResult<T>> SuccessAsync(List<T> data, int page, int pageSize)
    {
        return Task.FromResult(Success(data, page, pageSize));
    }
}