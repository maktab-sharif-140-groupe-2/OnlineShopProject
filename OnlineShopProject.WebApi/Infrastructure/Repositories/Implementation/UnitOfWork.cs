using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using OnlineShopProject.WebApi.Domain.Entities.RoleEntity;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;
using OnlineShopProject.WebApi.Infrastructure.Data;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

namespace OnlineShopProject.WebApi.Infrastructure.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
            ProductRepository = new ProductRepository(applicationDb);
            OrderRepository = new OrderRepository(applicationDb);
            OrderItemRepository=new OrderItemRepository(applicationDb);
        }
        private readonly ApplicationDbContext _applicationDb;

        private IDbContextTransaction?  Transaction { get; set; }
        public IProductRepository ProductRepository { get; set; }

        public IOrderItemRepository OrderItemRepository { get; private set; }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> _signInManager { get; set; }

        public RoleManager<Role> _roleManager { get; set; }

        public IOrderRepository OrderRepository { get; set; }

        public async Task<int> SaveAsync()
        {
            return await _applicationDb.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
           Transaction= await _applicationDb.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
          await Transaction!.CommitAsync();
        }

        public async Task RollBackTransactionAsync()
        {
            await Transaction!.RollbackAsync();
        }

    }
}
