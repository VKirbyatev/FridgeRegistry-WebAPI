using FridgeRegistry.Domain.UserProducts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FridgeRegistry.Infrastructure.Persistence.Configurations;

public class UserProductConfiguration : IEntityTypeConfiguration<UserProduct>
{
    public void Configure(EntityTypeBuilder<UserProduct> builder)
    {
        builder.HasKey(userProduct => userProduct.Id);
        builder.HasIndex(userProduct => userProduct.Id).IsUnique();

        builder
            .HasOne(userProduct => userProduct.Product)
            .WithMany()
            .IsRequired();
    }
}