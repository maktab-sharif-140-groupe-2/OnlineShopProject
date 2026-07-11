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
    }
}
