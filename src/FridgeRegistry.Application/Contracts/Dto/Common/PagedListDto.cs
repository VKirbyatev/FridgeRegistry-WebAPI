namespace FridgeRegistry.Application.Contracts.Dto.Common;

public class PagedListDto<T>
{
    public ICollection<T> Items { get; set; }
    
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}