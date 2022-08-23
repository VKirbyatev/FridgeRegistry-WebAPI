using FridgeRegistry.Application.Categories.Commands.CreateCategory;
using FridgeRegistry.Application.Categories.Commands.RemoveCategory;
using FridgeRegistry.Application.Categories.Commands.UpdateCategory;
using FridgeRegistry.Application.Categories.Queries.GetCategoryDescription;
using FridgeRegistry.Application.Categories.Queries.GetCategoryList;
using FridgeRegistry.Application.DTO.Categories;
using FridgeRegistry.WebAPI.Common.Constants;
using FridgeRegistry.WebAPI.Contracts;
using FridgeRegistry.WebAPI.Contracts.Requests.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FridgeRegistry.WebAPI.Controllers.V1;

public class CategoryController : BaseController
{
    [HttpGet(ApiRoutes.Category.GetList)]
    public async Task<ActionResult<ICollection<CategoryLookupDto>>> GetList()
    {
        var query = new GetCategoryListQuery();

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