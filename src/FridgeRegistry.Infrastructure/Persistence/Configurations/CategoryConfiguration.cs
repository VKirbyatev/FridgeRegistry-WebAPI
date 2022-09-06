using FridgeRegistry.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FridgeRegistry.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(category => category.Id);
        builder.HasIndex(category => category.Id).IsUnique();

        builder.HasIndex(category => category.Name).IsUnique();

        builder
            .HasOne(category => category.Parent)
            .WithMany(category => category.Children)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(category => category.Products)
            .WithOne(product => product.Category)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Metadata
            .FindNavigation("Children")
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder
            .Metadata
            .FindNavigation("Products")
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}