using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Authentications.Constants;
using OnlineShopProject.WebApi.Contracts.Dto;
using OnlineShopProject.WebApi.Entities.RoleEntity;
using OnlineShopProject.WebApi.Entities.UserEntity;

namespace OnlineShopProject.WebApi.Common.Extensions;

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

        await roleManager.AddClaimAsync(userRole, ClaimConstants.FreeUser);
        await roleManager.AddClaimAsync(userRole, ClaimConstants.ProUser);
        await roleManager.AddClaimAsync(userRole, ClaimConstants.ActiveStatus);
        await roleManager.AddClaimAsync(userRole, ClaimConstants.BannedStatus);
        await roleManager.AddClaimAsync(adminRole, ClaimConstants.ProUser);
    }

    private static async Task SeedAdminsAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var adminsData = configuration.GetSection("AdminData").Get<List<AdminData>>();

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