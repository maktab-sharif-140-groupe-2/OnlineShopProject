using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;

namespace OnlineShopProject.WebApi.Infrastructure.ModelBuilderConfigs;

public class UserModelBuilderConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(u=> u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

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
