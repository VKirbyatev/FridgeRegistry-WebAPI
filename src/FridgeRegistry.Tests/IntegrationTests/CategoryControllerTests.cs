using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using FridgeRegistry.Application.Categories.Commands.CreateCategory;
using FridgeRegistry.Application.Contracts.Dto.Categories;
using FridgeRegistry.Application.Contracts.Dto.Common;
using FridgeRegistry.WebAPI.Contracts;
using FridgeRegistry.WebAPI.Contracts.Requests.Category;

namespace FridgeRegistry.Tests.IntegrationTests;

public class CategoryControllerTests : IntegrationTest
{
    [Test]
    public async Task GetAll_WithoutAnyCategories_ReturnsEmptyList()
    {
        var client = CreateWebApiClient();
        await AuthenticateAsync(client);

        var pageSize = 10;

        var response = await client.GetAsync($"{ApiRoutes.Category.GetList}?Take={pageSize}");
        var responseObject = await response.Content.ReadFromJsonAsync<PagedListDto<CategoryLookupDto>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        responseObject!.Items.Should().BeEmpty();
        
        responseObject.TotalPages.Should().Be(0);
        responseObject.PageNumber.Should().Be(1);
        responseObject.PageSize.Should().Be(pageSize);
    }
    
    [Test]
    public async Task GetAll_WithExistingCategory_ReturnsListWithSingleCategory()
    {
        var client = CreateWebApiClient();
        await AuthenticateAsync(client);
        await CreateCategory(client);

        const int pageSize = 10;

        var response = await client.GetAsync($"{ApiRoutes.Category.GetList}?Take={pageSize}");
        var responseObject = await response.Content.ReadFromJsonAsync<PagedListDto<CategoryLookupDto>>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        responseObject!.Items.Count.Should().Be(1);
        responseObject.TotalPages.Should().Be(1);
        responseObject.PageNumber.Should().Be(1);
        responseObject.PageSize.Should().Be(pageSize);
    }
    
    [Test]
    public async Task GetDescription_WithExistingCategory_ReturnsCategory()
    {
        var client = CreateWebApiClient();
        await AuthenticateAsync(client);

        var categoryName = "Test";
        var categoryId = await CreateCategory(client, categoryName);

        var response = await client.GetAsync(
            ApiRoutes.Category.GetDescription.Replace("{id}", categoryId)
        );
        var responseObject = await response.Content.ReadFromJsonAsync<CategoryDescriptionDto>();

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        responseObject!.Children.Should().BeEmpty();
        responseObject.Id.Should().Be(categoryId);
        responseObject.Name.Should().Be(categoryName);
        responseObject.ParentId.Should().Be(Guid.Empty);
    }
    
    [Test]
    public async Task Create_ReturnsGuid()
    {
        var client = CreateWebApiClient();
        await AuthenticateAsync(client);
        
        const int guidLength = 36;

        var response = await client.PostAsJsonAsync(ApiRoutes.Category.Create, new CreateCategoryCommand()
        {
            Name = "Test Category"
        });
        var responseObject = (await response.Content.ReadFromJsonAsync<Guid>()).ToString();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseObject.Should().NotBeEmpty();
        responseObject.Length.Should().Be(guidLength);
    }
    
    [Test]
    public async Task Update_ReturnsVoid()
    {
        var client = CreateWebApiClient();
        await AuthenticateAsync(client);

        var categoryId = await CreateCategory(client);

        var response = await client.PutAsJsonAsync(
            ApiRoutes.Category.Update.Replace("{id}", categoryId),
            new UpdateCategoryRequest()
            {
                Name = "New Name"
            }
        );
        var responseObject = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseObject.Should().BeEmpty();
    }
    
    [Test]
    public async Task Remove_ReturnsVoid()
    {
        var client = CreateWebApiClient();
        await AuthenticateAsync(client);

        var categoryId = await CreateCategory(client);

        var response = await client.DeleteAsync(ApiRoutes.Category.Remove.Replace("{id}", categoryId));
        var responseObject = await response.Content.ReadAsStringAsync();

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        responseObject.Should().BeEmpty();
    }

    private async Task<string> CreateCategory(HttpClient client, string name = "Test Name")
    {
        var postResponse = await client.PostAsJsonAsync(ApiRoutes.Category.Create, new CreateCategoryCommand()
        {
            Name = name,
        });
        
        return (await postResponse.Content.ReadFromJsonAsync<Guid>()).ToString();
    }
}