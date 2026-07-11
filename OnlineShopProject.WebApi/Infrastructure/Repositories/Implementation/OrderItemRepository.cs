using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;
using OnlineShopProject.WebApi.Infrastructure.Data;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

namespace OnlineShopProject.WebApi.Infrastructure.Repositories.Implementation;

public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(ApplicationDbContext applicationDb) : base(applicationDb)
    {
    }
}
