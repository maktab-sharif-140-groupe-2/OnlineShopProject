using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

namespace OnlineShopProject.WebApi.Business.Services.Interface;

public interface IRoleService
{
    Task<bool> CreateRoleAsync(CreateRoleCommand createRoleCommand);

    Task<bool> DeleteRoleAsync(RemoveRoleCommand removeRoleCommand);

    Task<bool> AddRoleToUserAsync(AddRoleToUserCommand addRoleToUserCommand);
}
