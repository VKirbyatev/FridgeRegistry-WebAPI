using FridgeRegistry.Application.Categories.Commands.AddProductToCategory;
using FridgeRegistry.Application.Categories.Commands.CreateCategory;
using FridgeRegistry.Application.Categories.Commands.RemoveCategory;
using FridgeRegistry.Application.Categories.Commands.UpdateCategory;
using FridgeRegistry.Application.Categories.Queries.GetCategoryDescription;
using FridgeRegistry.Application.Categories.Queries.GetCategoryList;
using FridgeRegistry.Application.Categories.Queries.GetCategoryProductsList;
using FridgeRegistry.Application.Contracts.Dto.Categories;
using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.Application.Contracts.Dto.Products;
using FridgeRegistry.WebAPI.Common.Constants;
using FridgeRegistry.WebAPI.Contracts;
using FridgeRegistry.WebAPI.Contracts.Requests.Category;
using FridgeRegistry.WebAPI.Contracts.Requests.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FridgeRegistry.WebAPI.Controllers.V1;

public class CategoryController : BaseController
{
    [HttpGet(ApiRoutes.Category.GetList)]
    public async Task<ActionResult<PagedListDto<CategoryLookupDto>>> GetList([FromQuery] PagingRequest request)
    {
        var query = new GetCategoryListQuery()
        {
            SearchString = request.SearchString,
            
            Take = request.Take,
            Skip = request.Skip,
        };

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }
    
    [HttpGet(ApiRoutes.Category.GetProducts)]
    public async Task<ActionResult<PagedListDto<ProductLookupDto>>> GetProducts(Guid id, [FromQuery] PagingRequest request)
    {
        var query = new GetCategoryProductsListQuery()
        {
            CategoryId = id,
            
            SearchString = request.SearchString,
            Take = request.Take,
            Skip = request.Skip,
        };

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }

    [HttpGet(ApiRoutes.Category.GetDescription)]
    public async Task<ActionResult<CategoryDescriptionDto>> GetDescription(Guid id)
    {
        var query = new GetCategoryDescriptionQuery()
        {
            CategoryId = id,
        };

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }

    [HttpPost(ApiRoutes.Category.Create)]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<ActionResult<Guid>> Create(CreateCategoryCommand command)
    {
        var id = await Mediator.Send(command);
        return Ok(id);
    }
    
    [HttpPost(ApiRoutes.Category.AddProduct)]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<ActionResult> AddProduct(Guid categoryId, Guid productId)
    {
        var command = new AddProductToCategoryCommand()
        {
            CategoryId = categoryId,
            ProductId = productId
        };

        await Mediator.Send(command);
        return Ok();
    }

    [HttpPut(ApiRoutes.Category.Update)]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<ActionResult> Update(Guid id, UpdateCategoryRequest request)
    {
        var command = new UpdateCategoryCommand()
        {
            CategoryId = id,

            ParentCategoryId = request.ParentCategoryId,
            Name = request.Name,
        };

        await Mediator.Send(command);
        return Ok();
    }

    [HttpDelete(ApiRoutes.Category.Remove)]
    [Authorize(Roles = $"{Roles.Admin}")]
    public async Task<ActionResult> Remove(Guid id)
    {
        var command = new RemoveCategoryCommand()
        {
            CategoryId = id,
        };

        await Mediator.Send(command);
        return Ok();
    }
}