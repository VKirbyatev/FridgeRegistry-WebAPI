using MediatR;

namespace FridgeRegistry.Application.Products.Commands.RemoveProduct;

public class RemoveProductCommand : IRequest
{
    public Guid ProductId { get; set; }
}