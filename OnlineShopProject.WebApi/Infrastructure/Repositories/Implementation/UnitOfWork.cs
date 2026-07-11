using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Domain.Entities.RoleEntity;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;
using OnlineShopProject.WebApi.Infrastructure.Data;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

namespace OnlineShopProject.WebApi.Infrastructure.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        //public UnitOfWork(ApplicationDbContext applicationDb, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        //{
        //    _applicationDb = applicationDb;
        //    ProductRepository = new ProductRepository(applicationDb);
        //    UserManager = userManager;
        //    _signInManager = signInManager;
        //    _roleManager = roleManager;
        //}

        private readonly ApplicationDbContext _applicationDb;

        public UnitOfWork(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
            ProductRepository = new ProductRepository(applicationDb);
        }

        public IProductRepository ProductRepository { get; set; }

        public UserManager<User> UserManager { get; set; }

        public SignInManager<User> _signInManager { get; set; }

        public RoleManager<Role> _roleManager { get; set; }

        public async Task SaveAsync()
        {
           await _applicationDb.SaveChangesAsync();
        }
    }
}
