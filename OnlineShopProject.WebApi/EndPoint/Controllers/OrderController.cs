using Microsoft.AspNetCore.Authorization;
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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICurrentUser _currentUser;

        public OrderController(IOrderService orderService, ICurrentUser currentUser)
        {
            _orderService = orderService;
            _currentUser = currentUser;
        }

        [HttpPost]
        [Authorize(Policy = "ApplicationLogic")]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequestDto createOrderRequest)
        {
            await _orderService.CreateAsync(createOrderRequest.ToRequestDto(), _currentUser.UserId);

            return Ok(true);
        }

        [HttpGet("Orders")]
        [Authorize(Policy = "CanReadOrders")]
        public async Task<ActionResult<Pagination<OrderQuery>>> GetAll(int page, int pageSize)
        {
            var result = await _orderService.GetAllAsync(page, pageSize);
            return Ok(result);
        }

        [HttpGet("Orders{userId:Guid}")]
        [Authorize(Policy = "CanReadUserOrders")]
        public async Task<ActionResult<Pagination<OrderQuery>>> GetUserOrder([FromRoute] Guid userId, int page, int pageSize)
        {
            var result = await _orderService.GetUserOrdersAsync(userId, page, pageSize);
            return Ok(result);

        }

    }
}
