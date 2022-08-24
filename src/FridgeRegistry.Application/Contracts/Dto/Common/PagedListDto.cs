namespace FridgeRegistry.Application.Contracts.Dto.Common;

public class PagedListDto<T>
{
    public int TotalPages { get; set; }
    
    public ICollection<T> Items { get; set; }
}