using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Application.Interfaces;

public interface IDbContext
{
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    
    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}