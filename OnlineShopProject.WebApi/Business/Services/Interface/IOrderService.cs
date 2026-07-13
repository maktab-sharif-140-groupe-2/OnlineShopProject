using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Query;
using OnlineShopProject.WebApi.Domain.Common.Paginations;
using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;

namespace OnlineShopProject.WebApi.Business.Services.Interface;

public interface IOrderService
{
    Task CreateAsync(CreateOrderCommand createOrderCommand, Guid? createrId=null);

    Task<Pagination<OrderQuery>> GetAllAsync(int page, int pageSize);

    Task<Pagination<OrderQuery>> GetUserOrdersAsync(Guid userId,int page, int pageSize);


}
