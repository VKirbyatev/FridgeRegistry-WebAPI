using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.Application.Contracts.Dto.Products;
using FridgeRegistry.Application.Products.Commands.CreateProduct;
using FridgeRegistry.Application.Products.Commands.RemoveProduct;
using FridgeRegistry.Application.Products.Commands.UpdateProduct;
using FridgeRegistry.Application.Products.Queries.GetProductDescription;
using FridgeRegistry.Application.Products.Queries.GetProductsList;
using FridgeRegistry.WebAPI.Common.Constants;
using FridgeRegistry.WebAPI.Contracts;
using FridgeRegistry.WebAPI.Contracts.Requests.Common;
using FridgeRegistry.WebAPI.Contracts.Requests.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FridgeRegistry.WebAPI.Controllers.V1;

public class ProductController : BaseController
{
    /// <summary>
    /// Returns all existing products in app
    /// </summary>
    /// <response code="200">Returns all existing products in app</response>
    [HttpGet(ApiRoutes.Product.GetList)]
    public async Task<ActionResult<PagedListDto<ProductLookupDto>>> GetList([FromQuery] PagingRequest request)
    {
        var query = new GetProductsListQuery()
        {
            SearchString = request.SearchString,
            
            Take = request.Take,
            Skip = request.Skip,
        };

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }
    
    /// <summary>
    /// Returns full description about single product
    /// </summary>
    /// <response code="200">Returns full description about single product</response>
    /// <response code="404">Product with provided Id does not exists</response>
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
    
    /// <summary>
    /// Creates new product
    /// </summary>
    /// <response code="200">Creates new product</response>
    /// <response code="400">Unable to create new product due to validation errors</response>
    /// <response code="404">Product with provided Id does not exists</response>
    [HttpPost(ApiRoutes.Product.Create)]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<ActionResult<Guid>> Create(CreateProductCommand command)
    {
        var id = await Mediator.Send(command);
        return Ok(id);
    }
    
    /// <summary>
    /// Updates existing product
    /// </summary>
    /// <response code="200">Updates existing product</response>
    /// <response code="400">Unable to update product due to validation errors</response>
    /// <response code="404">Product with provided Id does not exists</response>
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
    
    /// <summary>
    /// Removes existing product
    /// </summary>
    /// <response code="200">Removes existing product</response>
    /// <response code="400">Unable to update product due to validation errors</response>
    /// <response code="404">Product with provided Id does not exists</response>
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