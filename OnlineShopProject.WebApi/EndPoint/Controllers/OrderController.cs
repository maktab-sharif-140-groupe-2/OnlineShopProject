using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.WebApi.Business.Services.Interface;
using OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Entity;
using OnlineShopProject.WebApi.EndPoint.Dto.RequestDto;

namespace OnlineShopProject.WebApi.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController:ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost()]
        public async Task Create(CreateOrderDto createorderDto,OrderDto orderDto)
        {
           var order= new OrderItem(createorderDto.OrderId, createorderDto.ProductId, createorderDto.Quantity);
            var result = _orderService.CreateAsync(orderDto.userId, order, orderDto.createrId);
        }
    }
}
