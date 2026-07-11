using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Query;
using OnlineShopProject.WebApi.Business.Exceptions;
using OnlineShopProject.WebApi.Business.Services.Interface;
using OnlineShopProject.WebApi.Domain.Common.Paginations;
using OnlineShopProject.WebApi.Domain.Entities.ProductEntity.Entity;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

namespace OnlineShopProject.WebApi.Business.Services.Implementation;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> CreateAsync(ProductCommand request)
    {
        var inspection = await _unitOfWork.ProductRepository.ExsitingAsync(x => x.Name == request.Name && x.BrandName == x.BrandName);
        if (inspection)
            throw new DouplicateException("product is exsit with same brand name");

        var product = new Product(request.Name, request.BrandName, request.Price, request.Stock);

        await _unitOfWork.ProductRepository.AddEntityAsync(product);

        await _unitOfWork.SaveAsync();

        return true;

    }

    public async Task<Pagination<ProductQuery>> GetAllAsync(int page, int pageSize)
    {
        var result = await _unitOfWork.ProductRepository.QueryAsync(x => true, x => new ProductQuery
        {
            BrandName = x.BrandName,
            Name = x.Name,
            Price = x.Price,
        }, page, pageSize);

        return result;

    }
}
