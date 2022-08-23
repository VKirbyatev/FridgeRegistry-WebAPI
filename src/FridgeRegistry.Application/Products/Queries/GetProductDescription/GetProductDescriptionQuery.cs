using FridgeRegistry.Application.DTO.Products;
using MediatR;

namespace FridgeRegistry.Application.Products.Queries.GetProductDescription;

public class GetProductDescriptionQuery : IRequest<ProductDescriptionDto>
{
    public Guid ProductId { get; set; }
}