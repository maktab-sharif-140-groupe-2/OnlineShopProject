using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
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

    public async Task CreateAsync(CreateOrderCommand createOrderCommand, Guid? createrId = null)
    {
        var user = await _unitOfWork.UserManager.FindByIdAsync(createOrderCommand.UserId.ToString());

        if (user == null)
            throw new NotFoundException("not exist the user in system");

        var product = await _unitOfWork.ProductRepository.GetEntityByIdAsync(createOrderCommand.ProductId);

        if (product == null)
            throw new NotFoundException("the product not exist in system");

        if (product.Stock + createOrderCommand.Quantity < 0)
            throw new InvalidQuantityException("product stock is not enough");

        var inspection = await _unitOfWork.OrderRepository
            .ExsitingAsync(o => o.UserId == createOrderCommand.UserId && o.Status == Status.Pending || o.Status == Status.Payment_Stage);

        if (inspection)
            throw new InvalidCreateOrderException("the user have a unhanled order");

        var order = new Order(createOrderCommand.UserId, createrId);

        await _unitOfWork.OrderRepository.AddEntityAsync(order);

        var orderItem = new OrderItem(order.Id, createOrderCommand.ProductId, createOrderCommand.Quantity, createrId);

        order.UpdatePrice(product.Price * createOrderCommand.Quantity);

        await _unitOfWork.OrderItemRepository.AddEntityAsync(orderItem);

        await _unitOfWork.SaveAsync();
    }

    public async Task<Pagination<OrderQuery>> GetAllAsync(int page, int pageSize)
    {
        var result = await _unitOfWork.OrderRepository.QueryAsync(x => true, x => new OrderQuery
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

    public async Task<Pagination<OrderQuery>> GetUserOrdersAsync(Guid userId, int page, int pageSize)
    {
        var result = await _unitOfWork.OrderRepository.QueryAsync(x => x.UserId == userId, x => new OrderQuery
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
