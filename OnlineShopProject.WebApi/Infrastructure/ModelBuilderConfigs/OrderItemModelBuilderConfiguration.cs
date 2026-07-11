using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;

namespace OnlineShopProject.WebApi.Infrastructure.ModelBuilderConfigs;

public class OrderItemModelBuilderConfiguration : BaseModelBuilderConfig<OrderItem>
{
    public override void ApplyConfiguration(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId);
    }
}
