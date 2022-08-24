using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Products;
using FridgeRegistry.Domain.UserProducts;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Contracts.Interfaces;

public interface IDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<UserProduct> UserProducts { get; set; }

    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}