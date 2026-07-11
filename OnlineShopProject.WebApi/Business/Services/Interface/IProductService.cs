using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Query;
using OnlineShopProject.WebApi.Domain.Common.Paginations;

namespace OnlineShopProject.WebApi.Business.Services.Interface;

public interface IProductService
{

    Task<bool> CreateAsync(ProductCommand request);

    Task<Pagination<ProductQuery>> GetAllAsync(int page,int pageSize);

}
