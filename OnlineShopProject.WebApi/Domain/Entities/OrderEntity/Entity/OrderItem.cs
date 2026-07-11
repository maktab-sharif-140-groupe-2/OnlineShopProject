using OnlineShopProject.WebApi.Domain.Entities.Abstractions;
using OnlineShopProject.WebApi.Domain.Entities.ProductEntity.Entity;

namespace OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;

public class OrderItem :BaseEntity
{
    public OrderItem(Guid orderId, Guid productId, int quantity,Guid? createrId=null)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
        createrId = null;
    }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; }


    public Product Product { get; private set; }
}
