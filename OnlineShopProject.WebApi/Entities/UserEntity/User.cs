using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Entities.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnlineShopProject.WebApi.Entities.UserEntity;

public class User : IdentityUser<Guid>, IEntity
{
    private User() { }

    public User(string fullName, int age, string email, string phoneNumber)
    {
        FullName = fullName;
        Age = age;
        Email = email;
        PhoneNumber = phoneNumber;
        UserName = Email;
    }

    public string FullName { get; set; }

    public int Age { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    private void Validate()
    {
    }

    public void SoftDelete()
    {
        DeletedAt = DateTime.UtcNow;
        IsDeleted = true;
        ModifiedAt = DateTime.UtcNow;
    }

    public void Update() => ModifiedAt = DateTime.UtcNow;

}
