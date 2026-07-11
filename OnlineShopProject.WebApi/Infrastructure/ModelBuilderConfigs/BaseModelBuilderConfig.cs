using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopProject.WebApi.Domain.Entities.Abstractions;

namespace OnlineShopProject.WebApi.Infrastructure.ModelBuilderConfigs;

public abstract class BaseModelBuilderConfig<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasQueryFilter(t=> !t.IsDeleted);

        builder.Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

       builder.HasOne(x => x.Creater)
                     .WithMany()
                     .HasForeignKey(u => u.CreaterId)
                     .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Deleter)
        .WithMany()
        .HasForeignKey(u => u.DeleterId)
        .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.Modifieder)
        .WithMany()
        .HasForeignKey(u => u.ModifiederId)
        .OnDelete(DeleteBehavior.NoAction);


        ApplyConfiguration(builder);
    }
    public abstract void ApplyConfiguration(EntityTypeBuilder<T> builder);
}
