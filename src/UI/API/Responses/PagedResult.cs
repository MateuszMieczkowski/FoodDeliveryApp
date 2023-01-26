namespace UI.API.Responses;

public class PagedResult<T>
{
    public int TotalCount { get; set; }

    public int PageCount { get; set; }

    public int PageSize { get; set; }

    public int CurrentPageNumber { get; set; }

    public List<T> Result { get; set; } = default!;

}