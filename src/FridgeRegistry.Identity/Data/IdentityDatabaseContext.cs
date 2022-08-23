using FridgeRegistry.Identity.Data.Configurations;
using FridgeRegistry.Identity.Data.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FridgeRegistry.Identity.Data;

public class IdentityDatabaseContext : IdentityDbContext
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public IdentityDatabaseContext(DbContextOptions<IdentityDatabaseContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}