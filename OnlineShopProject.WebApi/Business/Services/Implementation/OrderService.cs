using OnlineShopProject.WebApi.Business.Contracts.Dto.Query;
using OnlineShopProject.WebApi.Business.Exceptions;
using OnlineShopProject.WebApi.Business.Services.Interface;
using OnlineShopProject.WebApi.Domain.Common.Paginations;
using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;
using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Enums;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

namespace OnlineShopProject.WebApi.Business.Services.Implementation;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(Guid userId, OrderItem initOrderItem, Guid? createrId = null)
    {
        var user = await _unitOfWork.UserManager.FindByIdAsync(userId.ToString());
        if (user == null)
            throw new NotFoundException("not exist the user in system");

        
        var inspection= await _unitOfWork.OrderRepository
            .ExsitingAsync(o=> o.UserId == userId && o.Status==Status.Pending|| o.Status == Status.Payment_Stage);

        if (inspection)
            throw new InvalidCreateOrderException("the user have a unhanled order");


        var order = new Order(userId, createrId);

        await _unitOfWork.OrderRepository.AddEntityAsync(order);

        var orderItem = new OrderItem(order.Id, initOrderItem.ProductId, initOrderItem.Quantity,createrId);

        await _unitOfWork.OrderItemRepository.AddEntityAsync(orderItem);

        await _unitOfWork.SaveAsync();

    }

    public Task<Pagination<ProductQuery>> GetAllAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<Pagination<ProductQuery>> GetUserOrdersAsync(int page, int pageSize)
    {
        throw new NotImplementedException();
    }
}
