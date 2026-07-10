using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Entities.Abstractions;

namespace OnlineShopProject.WebApi.Entities.RoleEntity;

public class Role : IdentityRole<Guid>, IEntity
{
    private Role() { }

    public Role(string name, string? description = null)
    {
        Name = name;
        Description = description;

        Validate();
    }

    public string? Description { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? ModifiedAt { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    public bool IsDeleted { get; private set; }

    public void SoftDelete()
    {
        DeletedAt = DateTime.UtcNow;
        IsDeleted = true;
        ModifiedAt = DateTime.UtcNow;
    }

    public void Update() => ModifiedAt = DateTime.UtcNow;

    private void Validate()
    {
    }
}
