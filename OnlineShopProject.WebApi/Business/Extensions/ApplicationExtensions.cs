using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Authentications.Constants;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Domain.Entities.RoleEntity;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;

namespace OnlineShopProject.WebApi.Business.Extensions;

public static class ApplicationExtensions
{
    public static async Task SeedDataBaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        await SeedRolesAsync(scope.ServiceProvider);
        await SeedAdminsAsync(scope.ServiceProvider);
    }

    private static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        if (roleManager.Roles.Any()) return;

        var adminRole = new Role(RoleConstants.AdminRoleName);
        var userRole = new Role(RoleConstants.UserRoleName);

        await roleManager.CreateAsync(adminRole);
        await roleManager.CreateAsync(userRole);

        await roleManager.AddClaimAsync(userRole, ClaimConstants.ReadProduct);
        await roleManager.AddClaimAsync(userRole, ClaimConstants.CreateOrder);
        await roleManager.AddClaimAsync(adminRole, ClaimConstants.CreateProduct);
        await roleManager.AddClaimAsync(adminRole, ClaimConstants.ChangeProduct);
        await roleManager.AddClaimAsync(adminRole, ClaimConstants.DeleteProduct);
        await roleManager.AddClaimAsync(adminRole, ClaimConstants.ReadProduct);
        await roleManager.AddClaimAsync(adminRole, ClaimConstants.ReadOrder);
    }   

    private static async Task SeedAdminsAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var adminsData = configuration.GetSection("AdminData").Get<List<AdminCommand>>();

        if (!adminsData?.Any() ?? true) return;

        var adminData = adminsData.FirstOrDefault(d => d.Role == RoleConstants.AdminRoleName);

        if (adminData != null)
        {
            var adminUser = new User(adminData.FullName, 21, adminData.Email, adminData.PhoneNumber);
            await userManager.CreateAsync(adminUser, adminData.Password);
            await userManager.AddToRoleAsync(adminUser, RoleConstants.AdminRoleName);
        }
    }
}