namespace Web.Api.Models;

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

    public int TotalCount { get; set; }

    public int PageCount { get; set; }

    public int PageSize { get; set; }

    public int CurrentPageNumber { get; set; }

    public List<T> Result { get; set; }

}
