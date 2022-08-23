using FridgeRegistry.Application.DTO.Products;
using FridgeRegistry.Application.Products.Commands.CreateProduct;
using FridgeRegistry.Application.Products.Commands.RemoveProduct;
using FridgeRegistry.Application.Products.Commands.UpdateProduct;
using FridgeRegistry.Application.Products.Queries.GetProductDescription;
using FridgeRegistry.Application.Products.Queries.GetProductsList;
using FridgeRegistry.WebAPI.Common.Constants;
using FridgeRegistry.WebAPI.Contracts;
using FridgeRegistry.WebAPI.Contracts.Requests.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FridgeRegistry.WebAPI.Controllers.V1;

public class ProductController : BaseController
{
    [HttpGet(ApiRoutes.Product.GetList)]
    public async Task<ActionResult<ICollection<ProductLookupDto>>> GetList()
    {
        var query = new GetProductsListQuery();

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }
    
    [HttpGet(ApiRoutes.Product.GetDescription)]
    public async Task<ActionResult<ProductDescriptionDto>> GetDescription(Guid id)
    {
        var query = new GetProductDescriptionQuery()
        {
            ProductId = id,
        };

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }
    
    [HttpPost(ApiRoutes.Product.Create)]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<ActionResult<Guid>> Create(CreateProductCommand command)
    {
        var id = await Mediator.Send(command);
        return Ok(id);
    }
    
    [HttpPut(ApiRoutes.Product.Update)]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<ActionResult> Update(Guid id, UpdateProductRequest request)
    {
        var command = new UpdateProductCommand()
        {
            ProductId = id,

            Name = request.Name,
            Description = request.Description,
            ShelfLife = request.ShelfLife,
        };
        
        await Mediator.Send(command);
        return Ok();
    }
    
    [HttpDelete(ApiRoutes.Product.Remove)]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<ActionResult> Remove(Guid id)
    {
        var command = new RemoveProductCommand()
        {
            ProductId = id,
        };
        
        await Mediator.Send(command);
        return Ok();
    }
}