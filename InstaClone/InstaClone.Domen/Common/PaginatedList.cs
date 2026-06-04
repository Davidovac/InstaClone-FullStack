namespace InstaClone.Domain.Common;

public class PaginatedList<T>
{
    public List<T> Items { get; set; }
    public int Count { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
    public PaginatedList(List<T> items, int currentPage, int pageSize, int count)
    {
        Items = items;
        Count = count;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        HasPreviousPage = CurrentPage > 1;
        HasNextPage = CurrentPage < TotalPages;
    }

    public PaginatedList()
    {
        Items = new List<T>();
    }
}