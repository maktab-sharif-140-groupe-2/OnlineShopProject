using OnlineShopProject.WebApi.Domain.Entities.ProductEntity.Entity;

namespace OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

public interface IProductRepository : IGenericRepository<Product>
{
    Task UpdatePriceAsync(Product product,decimal newPrice,Guid modifierId);

    Task UpdateStockAsync(Product product,int stock, Guid modifierId);

}
