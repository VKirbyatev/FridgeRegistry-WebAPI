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