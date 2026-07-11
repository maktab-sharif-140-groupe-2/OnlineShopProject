using OnlineShopProject.WebApi.Domain.Entities.Abstractions;
using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Excepions;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;

namespace OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;

public class Order : BaseEntity
{
    public Order(Guid userId,Guid? createrId=null)
    {
        UserId = userId;
        TotalPrice =0;
        CreaterId = createrId;
    }

    public Guid UserId { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateOnly? DeliveryDate { get; private set; }

    public List<OrderItem> Items { get; private set; } = [];
    public User User { get; private set; }

    public void SetDeliveryDate(DateOnly deliveryDate)
    {
        if (deliveryDate <= DateOnly.FromDateTime(DateTime.Now))
            throw new DeliveryDataException("delivery time most be in future");

        DeliveryDate = deliveryDate;
    }

}
