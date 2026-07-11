using Microsoft.AspNetCore.DataProtection.Repositories;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;

namespace OnlineShopProject.WebApi.Domain.Entities.Abstractions;

public abstract class BaseEntity : IEntity
{

    /// <summary>
    /// شناسه موجودیت
    /// </summary>
    public Guid Id { get; private set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreaterId { get; set; }

    public Guid? ModifiederId { get; set; }

    public Guid? DeleterId { get; set; }
    public User? Creater { get; set; }
    public User? Modifieder { get; set; }
    public User? Deleter { get; set; }

    public void SoftDelete(Guid deleterId)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeleterId = deleterId;
    }

    public void Update(Guid modifiederId)
    {
        ModifiedAt = DateTime.UtcNow;
        ModifiederId = modifiederId;
    }
}
