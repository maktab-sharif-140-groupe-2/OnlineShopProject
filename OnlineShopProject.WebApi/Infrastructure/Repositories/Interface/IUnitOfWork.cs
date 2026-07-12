using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Domain.Entities.RoleEntity;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;

namespace OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

public interface IUnitOfWork 
{
    IProductRepository ProductRepository { get; }
    IOrderRepository OrderRepository { get; }

    UserManager<User> UserManager { get; }

    SignInManager<User> _signInManager { get; }

    RoleManager<Role> _roleManager { get; }

    IOrderItemRepository OrderItemRepository { get;   }

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollBackTransactionAsync();
    Task<int> SaveAsync();

}
