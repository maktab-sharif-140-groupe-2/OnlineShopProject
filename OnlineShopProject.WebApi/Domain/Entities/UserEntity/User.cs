using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Domain.Entities.Abstractions;
using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity.Enums;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnlineShopProject.WebApi.Domain.Entities.UserEntity;

public class User : IdentityUser<Guid>, IEntity
{
    private User() { }

    public User(string fullName, int age, string email, string phoneNumber,Guid? createrId=null)
    {
        FullName = fullName;
        Age = age;
        Email = email;
        PhoneNumber = phoneNumber;
        UserName = Email;
        Plan = Plan.Free;
        Status = BanStatus.Active;
        CreaterId= createrId;
    }

    public string FullName { get; set; }

    public int Age { get; set; }

    public List<Order> Orders { get; private set; } = [];

    public Plan Plan { get; private set; }
    public BanStatus Status { get; private set; }

    public DateTime? BanTime { get; set; }

    public DateOnly? LastPermium { get; private set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool IsDeleted { get; set; }

    public Guid? CreaterId { get;  set; }

    public Guid? ModifiederId { get;  set; }

    public Guid? DeleterId { get;  set; }

    public User? Creater { get; set; }

    public User? Modifieder { get; set; }

    public User? Deleter { get; set; }

    public void ToProPlan()
    {
        Plan = Plan.Pro;
        LastPermium=DateOnly.FromDateTime(DateTime.UtcNow);
    }
    public void ToFreePlan()
    {
        if (Plan==Plan.Pro&&LastPermium!=null&&LastPermium.Value.AddDays(30)< DateOnly.FromDateTime(DateTime.UtcNow))
        {
           LastPermium = null;
           Plan = Plan.Free;
        }

    }
    public void Banning(Guid modifiederId)
    {
        Status = BanStatus.Banned;
        BanTime = DateTime.UtcNow;
        Update(modifiederId);
    }
    public void UnBanning(Guid modifiederId)
    {
        Status = BanStatus.Active;
        BanTime = null;
        Update(modifiederId);   
    }
    public void UnBanning()
    {
        if (BanTime != null && BanTime.Value.AddDays(20) < DateTime.UtcNow)
        {
        Status = BanStatus.Active;
        BanTime = null;
        }
    }
    public void SoftDelete()
    {
        DeletedAt = DateTime.UtcNow;
        IsDeleted = true;
        ModifiedAt = DateTime.UtcNow;
    }


    public void SoftDelete(Guid deleterId)
    {
        DeleterId= deleterId;
        IsDeleted = true;
        DeletedAt= DateTime.UtcNow;
    }

    public void Update(Guid modifiederId)
    {
        ModifiedAt = DateTime.UtcNow;
        ModifiederId= modifiederId;
    }
}
