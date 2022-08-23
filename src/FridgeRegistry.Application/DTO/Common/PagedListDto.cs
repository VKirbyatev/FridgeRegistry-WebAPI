namespace FridgeRegistry.Application.DTO.Common;

public class PagedListDto<T>
{
    public int TotalPages { get; set; }
    
    public ICollection<T> Items { get; set; }
}