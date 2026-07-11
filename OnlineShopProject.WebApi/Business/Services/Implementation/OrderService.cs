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

        var product = await _unitOfWork.ProductRepository.GetEntityByIdAsync(initOrderItem.ProductId);

        if (product == null)
            throw new NotFoundException("the product not exist in system");

        if (product.Stock + initOrderItem.Quantity < 0)
            throw new InvalidQuantityException("product stock is not enough");

        var orderItem = new OrderItem(order.Id, initOrderItem.ProductId, initOrderItem.Quantity,createrId);

         order.UpdatePrice(product.Price* initOrderItem.Quantity);

        await _unitOfWork.OrderItemRepository.AddEntityAsync(orderItem);

        await _unitOfWork.SaveAsync();
    }

    public async Task<Pagination<OrderQuery>> GetAllAsync(int page, int pageSize)
    {
        var result = await _unitOfWork.OrderRepository.QueryAsync(x => true, x => new OrderQuery
        {
            TotalPrice=x.TotalPrice,
            Status=x.Status.ToString(),
            DeliveryDate=x.DeliveryDate,
            OrdererName=x.User.FullName,
            Items=x.Items.Select(p=> new ProductQuery
            {
                BrandName=p.Product.BrandName,
                Name=p.Product.Name,
                Price=p.Product.Price
            }).ToList()
        }, page, pageSize);

        return result;
    }

    public async Task<Pagination<OrderQuery>> GetUserOrdersAsync(Guid userId,int page, int pageSize)
    {
        var result = await _unitOfWork.OrderRepository.QueryAsync(x => x.UserId==userId, x => new OrderQuery
        {
            TotalPrice = x.TotalPrice,
            Status = x.Status.ToString(),
            DeliveryDate = x.DeliveryDate,
            OrdererName = x.User.FullName,
            Items = x.Items.Select(p => new ProductQuery
            {
                BrandName = p.Product.BrandName,
                Name = p.Product.Name,
                Price = p.Product.Price
            }).ToList()
        }, page, pageSize);

        return result;
    }
}
