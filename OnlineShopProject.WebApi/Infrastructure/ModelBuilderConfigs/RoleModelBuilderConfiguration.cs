using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopProject.WebApi.Domain.Entities.RoleEntity;

namespace OnlineShopProject.WebApi.Infrastructure.ModelBuilderConfigs
{
    public class RoleModelBuilderConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
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
        }
    }
}
