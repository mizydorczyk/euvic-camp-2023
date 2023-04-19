namespace API.Models;

public class PagedResult<T>
{
    public PagedResult(List<T> items, int totalCount, int pageSize, int pageNumber)
    {
        Items = items;
        TotalItemsCount = totalCount;
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = Math.Min(ItemsFrom + pageSize - 1, totalCount);
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
    }

    public List<T> Items { get; set; }
    public int TotalPages { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
    public int TotalItemsCount { get; set; }
}