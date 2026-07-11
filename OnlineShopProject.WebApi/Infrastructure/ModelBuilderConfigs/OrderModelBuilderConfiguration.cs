using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;
using OnlineShopProject.WebApi.Infrastructure.ModelBuilderConfigs;

namespace OnlineShopProject.WebApi.Infrastructure.ModelBuilderConfigsk;

public class OrderModelBuilderConfiguration : BaseModelBuilderConfig<Order>
{
    public override void ApplyConfiguration(EntityTypeBuilder<Order> builder)
    {

        builder.HasMany(o => o.Items)
            .WithOne()
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(o => o.TotalPrice)
            .HasColumnType("decimal(16,2)") ;
    }
}
