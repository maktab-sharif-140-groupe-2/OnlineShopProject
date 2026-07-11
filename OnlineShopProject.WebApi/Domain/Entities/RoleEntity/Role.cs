using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Domain.Entities.Abstractions;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;

namespace OnlineShopProject.WebApi.Domain.Entities.RoleEntity;

public class Role : IdentityRole<Guid>, IEntity
{
    private Role() { }

    public Role(string name, string? description = null, Guid? createrId = null)
    {
        Name = name;
        Description = description;
        CreaterId = createrId;
        Validate();
    }

    public string? Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? ModifiedAt { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    public bool IsDeleted { get; private set; }

    public Guid? CreaterId { get; private set; }

    public Guid? ModifiederId { get; private set; }

    public Guid? DeleterId { get; private set; }

    public User? Creater { get; private set; }

    public User? Modifieder { get; private set; }

    public User? Deleter { get; private set; }

    public void SoftDelete()
    {
        DeletedAt = DateTime.UtcNow;
        IsDeleted = true;
        ModifiedAt = DateTime.UtcNow;
    }

    private void Validate()
    {
    }

    public void SoftDelete(Guid deleterId)
    {
        DeleterId= deleterId;
        IsDeleted = true;
        DeletedAt= DateTime.UtcNow;
    }

    public void Update(Guid modifiederId)
    {
        ModifiederId= modifiederId;
        ModifiedAt= DateTime.UtcNow;
    }
}
