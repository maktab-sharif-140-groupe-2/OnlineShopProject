using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopProject.WebApi.Domain.Entities.ProductEntity.Entity;

namespace OnlineShopProject.WebApi.Infrastructure.ModelBuilderConfigs;

public class ProductModelBuilderConfiguration : BaseModelBuilderConfig<Product>
{
    public override void ApplyConfiguration(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Price)
         .HasColumnType("decimal(18,5)");
    }
}
