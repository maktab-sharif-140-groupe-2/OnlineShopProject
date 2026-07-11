using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Authentications.Constants;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Business.Services.Interface;
using OnlineShopProject.WebApi.Domain.Entities.RoleEntity;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;

namespace OnlineShopProject.WebApi.Business.Services.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddRoleToUserAsync(AddRoleToUserCommand addRoleToUserCommand)
        {
            await ChekingPermissionAsync(addRoleToUserCommand.RequesterId);

            var foundedRole = await _unitOfWork._roleManager.FindByIdAsync(addRoleToUserCommand.RoleId.ToString());

            if (foundedRole is null)
                throw new NotFoundException("مشتیییی این وجود نداره");

            var foundedUser = await _unitOfWork.UserManager.FindByIdAsync(addRoleToUserCommand.UserId.ToString());

            if (foundedUser is null)
                throw new NotFoundException("user not found");

            var result = await _unitOfWork.UserManager.AddToRoleAsync(foundedUser, foundedRole.Name!);

            return result == IdentityResult.Success;
        }

        public async Task<bool> CreateRoleAsync(CreateRoleCommand createRoleCommand)
        {
            var requester = await ChekingPermissionAsync(createRoleCommand.RequesterId);

            var roleExist = await _unitOfWork._roleManager.RoleExistsAsync(createRoleCommand.Name);

            if (roleExist)
                throw new ConflictException("مشتی این روله وجود داره چی میزنی بده مام بزنیم!!!!");

            var role = new Role(createRoleCommand.Name, null, requester.Id);

            var result = await _unitOfWork._roleManager.CreateAsync(role);

            return result == IdentityResult.Success;
        }

        public async Task<bool> DeleteRoleAsync(RemoveRoleCommand removeRoleCommand)
        {
            var foundedRole = await _unitOfWork._roleManager.FindByIdAsync(removeRoleCommand.RoleId.ToString());

            if (foundedRole is null)
                throw new NotFoundException("مشتیییی این وجود نداره");

            var requester = await ChekingPermissionAsync(removeRoleCommand.RequesterId);

            foundedRole.SoftDelete(requester.Id);

            var result = await _unitOfWork._roleManager.UpdateAsync(foundedRole);

            return result == IdentityResult.Success;
        }

        private async Task<User> ChekingPermissionAsync(Guid requesterId)
        {
            var requester = await _unitOfWork.UserManager.FindByIdAsync(requesterId.ToString());

            if (requester == null)
                throw new PermissionDeniedException("the requester not found");

            var requesterRoles = await _unitOfWork.UserManager.GetRolesAsync(requester);

            if (!requesterRoles.Contains(RoleConstants.AdminRoleName))
                throw new PermissionDeniedException("you must be the admin in the system");

            return requester;
        }
    }
}
