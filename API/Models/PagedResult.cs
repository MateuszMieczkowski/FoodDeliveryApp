namespace API.Models;

public class PagedResult<T>
{
    public PagedResult(List<T> result, int pageNumber, int pageSize, int totalCount)
    {
        Result = result;
        CurrentPageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        PageCount = Convert.ToInt32(Math.Ceiling(totalCount / (decimal)pageSize));
    }

    public int TotalCount { get; }

    public int PageCount { get; }

    public int PageSize { get; }

    public int CurrentPageNumber { get; }

    public List<T> Result { get; }

}
