using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Products;
using FridgeRegistry.Domain.UserProducts;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Contracts.Interfaces;

public interface IDbContext
{
    DbSet<Product> Products { get; }
    DbSet<Category> Categories { get; }
    DbSet<UserProduct> UserProducts { get; }

    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}