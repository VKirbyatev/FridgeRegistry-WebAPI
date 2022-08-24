using FridgeRegistry.Application.Contracts.Interfaces;
using FridgeRegistry.Domain.Categories;
using FridgeRegistry.Domain.Products;
using FridgeRegistry.Domain.UserProducts;
using FridgeRegistry.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Infrastructure.Persistence;

public class FridgeRegistryDbContext : DbContext, IDbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<UserProduct> UserProducts { get; set; }

    public FridgeRegistryDbContext(DbContextOptions<FridgeRegistryDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new UserProductConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync(CancellationToken.None);
    }
}