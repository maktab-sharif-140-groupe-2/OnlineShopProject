using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Query;
using OnlineShopProject.WebApi.Business.Services.Interface;
using OnlineShopProject.WebApi.Domain.Common.Paginations;
using OnlineShopProject.WebApi.EndPoint.Dto.RequestDto;

namespace OnlineShopProject.WebApi.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        //[Authorize(Policy = "CanCreateProduct")]
        public async Task<IActionResult> CreateAsync([FromBody] ProductRequest request)
        {
            Console.WriteLine();
            var result = await _productService.CreateAsync(request.ToCommand());
            return Created();
        }

        [HttpGet]
        //[Authorize(Policy = "ApplicationLogic")]
        public async Task<ActionResult<Pagination<ProductQuery>>> GetProducts([FromQuery] int pageNumber=1,[FromQuery] int size=10)
        {
            var result= await _productService.GetAllAsync(pageNumber, size);
            return result;
        }

        [HttpDelete("Remove")]
        //[Authorize(Policy = "CanDeleteProduct")]
        public async Task<ActionResult<bool>> RemoveProduct(Guid productId, Guid deleterId)
        {
            var result=await _productService.RemoveProductAsync(productId, deleterId);
            return result;
        }

        [HttpPatch("UpdatePrice")]
        //[Authorize(Policy = "CanChangeProduct")]
        public async Task<ActionResult<bool>> UpdatePrice(UpdatePriceRequest updatePrice)
        {
            var result = await _productService.UpdateProductPriceAsync(updatePrice.ProductId, updatePrice.NewPrice, updatePrice.ModifiederId);
            return result;

        }

        [HttpPatch("UpdateStock")]
        //[Authorize(Policy = "CanChangeProduct")]
        public async Task<ActionResult<bool>> UpdateStock(UpdateStockRequest updateStock)
        {
            var result = await _productService.UpdateProductStockAsync(updateStock.productId,updateStock.stock,updateStock.modifierId);
            return result;
        }
      
    }
}
