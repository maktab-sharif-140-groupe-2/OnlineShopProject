using OnlineShopProject.WebApi.Business.Contracts.Dto.Query;
using OnlineShopProject.WebApi.Domain.Common.Paginations;
using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;

namespace OnlineShopProject.WebApi.Business.Services.Interface;

public interface IOrderService
{
    Task CreateAsync(Guid userId,OrderItem initOrderItem,Guid? createrId=null);

    Task<Pagination<ProductQuery>> GetAllAsync(int page, int pageSize);

    Task<Pagination<ProductQuery>> GetUserOrdersAsync(int page, int pageSize);


}
