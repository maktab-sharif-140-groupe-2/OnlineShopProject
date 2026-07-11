using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Query;
using OnlineShopProject.WebApi.Business.Services.Interface;
using OnlineShopProject.WebApi.Domain.Common.Paginations;
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
        [HttpGet("Orders")]
        public async Task<ActionResult<Pagination<OrderQuery>>> GetAll(int page, int pageSize)
        {
            var result=await _orderService.GetAllAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("Orders{Guid:UserId}")]
        public async Task<ActionResult<Pagination<OrderQuery>>> GetUserOrder([FromRoute]Guid userId, int page, int pageSize)
        {
            var result= await _orderService.GetUserOrdersAsync(userId, page, pageSize);
            return Ok(result);

        }

    }
}
