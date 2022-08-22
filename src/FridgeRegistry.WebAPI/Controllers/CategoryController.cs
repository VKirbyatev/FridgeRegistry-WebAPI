using FridgeRegistry.Application.Categories.Commands.CreateCategory;
using FridgeRegistry.Application.Categories.Commands.RemoveCategory;
using FridgeRegistry.Application.Categories.Commands.UpdateCategory;
using FridgeRegistry.Application.Categories.Queries.GetCategoryDescription;
using FridgeRegistry.Application.Categories.Queries.GetCategoryList;
using FridgeRegistry.Application.DTO.Categories;
using FridgeRegistry.WebAPI.Requests.Category;
using Microsoft.AspNetCore.Mvc;

namespace FridgeRegistry.WebAPI.Controllers;

[Route("api/[controller]")]
public class CategoryController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ICollection<CategoryLookupDto>>> GetList()
    {
        var query = new GetCategoryListQuery();

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDescriptionDto>> GetDescription(Guid id)
    {
        var query = new GetCategoryDescriptionQuery()
        {
            CategoryId = id,
        };

        var dto = await Mediator.Send(query);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(CreateCategoryCommand command)
    {
        var id = await Mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
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