using OnlineShopProject.WebApi.Domain.Entities.ProductEntity.Entity;
using OnlineShopProject.WebApi.Infrastructure.Data;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

namespace OnlineShopProject.WebApi.Infrastructure.Repositories.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext applicationDb) : base(applicationDb)
        {
        }

        public async Task UpdatePriceAsync(Product product, decimal newPrice, Guid modifierId)
        {
            product.ChangePrice(newPrice,modifierId);
        }

        public async Task UpdateStockAsync(Product product, int stock, Guid modifierId)
        {
            product.ChangeStock(stock,modifierId);
        }
    }
}
