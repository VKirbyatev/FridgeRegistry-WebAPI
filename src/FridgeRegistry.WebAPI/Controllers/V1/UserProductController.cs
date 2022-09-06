using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.Application.Contracts.Dto.UserProduct;
using FridgeRegistry.Application.UserProducts.Commands.CreateUserProduct;
using FridgeRegistry.Application.UserProducts.Commands.RemoveUserProduct;
using FridgeRegistry.Application.UserProducts.Commands.UpdateUserProduct;
using FridgeRegistry.Application.UserProducts.Queries.GetUserProductDescription;
using FridgeRegistry.Application.UserProducts.Queries.GetUserProductList;
using FridgeRegistry.WebAPI.Common.Extensions.HttpExtensions;
using FridgeRegistry.WebAPI.Contracts;
using FridgeRegistry.WebAPI.Contracts.Requests.UserProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FridgeRegistry.WebAPI.Controllers.V1;

public class UserProductController : BaseController
{
    /// <summary>
    /// Return all user's products
    /// </summary>
    /// <param name="request.SortType">ASC (Ascending) or DESC (Descending) sort types</param>
    /// <param name="request.SortBy">Available sorting methods: [ "Name", "Expiration_Date" ]</param>
    /// <response code="200">Return all user's products</response>
    [HttpGet(ApiRoutes.UserProducts.GetList)]
    [Authorize]
    public async Task<ActionResult<PagedListDto<UserProductLookupDto>>> GetList([FromQuery] GetUserProductListRequest request)
    {
        var userId = HttpContext.GetUserId();
        
        var query = new GetUserProductListQuery()
        {
            UserId = userId,
            
            SearchString = request.SearchString,
            
            Take = request.Take,
            Skip = request.Skip,
            
            SortType = request.SortType,
            SortBy = request.SortBy,
        };

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }
    
    /// <summary>
    /// Return full description about single user's product
    /// </summary>
    /// <response code="200">Return full description about single user's product</response>
    /// <response code="400">Unable to find product due to validation errors</response>
    /// <response code="404">Product with provided Id does not exists</response>
    [HttpGet(ApiRoutes.UserProducts.GetDescription)]
    [Authorize]
    public async Task<ActionResult<UserProductDescriptionDto>> GetDescription(Guid id)
    {
        var userId = HttpContext.GetUserId();
        
        var query = new GetUserProductDescriptionQuery()
        {
            UserProductId = id,
            UserId = userId,
        };

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }
    
    /// <summary>
    /// Creates new product in user's collection
    /// </summary>
    /// <param name="request.QuantityType">Enumeration type of quantity type. (E.g. Kilograms, Pounds, Items etc.)</param>
    /// <response code="200">Creates new product in user's collection</response>
    /// <response code="400">Unable to create new product due to validation errors</response>
    [HttpPost(ApiRoutes.UserProducts.Create)]
    [Authorize]
    public async Task<ActionResult<Guid>> Create(CreateUserProductRequest request)
    {
        var userId = HttpContext.GetUserId();
        
        var command = new CreateUserProductCommand()
        {
            UserId = userId,
            ProductId = request.ProductId,
            
            Commentary = request.Commentary,
            
            ProductionDate = request.ProductionDate,
            
            Quantity = request.Quantity,
            QuantityType = request.QuantityType,
        };

        var id = await Mediator.Send(command);
        return Ok(id);
    }
    
    /// <summary>
    /// Updates product in user's collection
    /// </summary>
    /// <param name="request.QuantityType">Enumeration type of quantity type. (E.g. Kilograms, Pounds, Items etc.)</param>
    /// <response code="200">Updates product in user's collection</response>
    /// <response code="400">Unable to update product due to validation errors</response>
    /// /// <response code="403">Forbidden action, only product's creator allowed to do that</response>
    /// <response code="404">Product with provided Id does not exists</response>
    [HttpPut(ApiRoutes.UserProducts.Update)]
    [Authorize]
    public async Task<ActionResult> Update(Guid id, UpdateUserProductRequest request)
    {
        var userId = HttpContext.GetUserId();
        
        var command = new UpdateUserProductCommand()
        {
            UserId = userId,
            UserProductId = id,
            
            Commentary = request.Commentary,
            
            ProductionDate = request.ProductionDate,
            
            Quantity = request.Quantity,
            QuantityType = request.QuantityType,
        };

        await Mediator.Send(command);
        return Ok();
    }
    
    /// <summary>
    /// Remove product from user's collection
    /// </summary>
    /// <response code="200">Remove product from user's collection</response>
    /// <response code="403">Forbidden action, only product's creator allowed to do that</response>
    /// <response code="404">Product with provided Id does not exists</response>
    [HttpDelete(ApiRoutes.UserProducts.Remove)]
    [Authorize]
    public async Task<ActionResult> Remove(Guid id)
    {
        var userId = HttpContext.GetUserId();
        
        var command = new RemoveUserProductCommand()
        {
            UserId = userId,
            UserProductId = id,
        };

        await Mediator.Send(command);
        return Ok();
    }
}