using Microsoft.AspNetCore.Identity;
using OnlineShopProject.WebApi.Authentications.Constants;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Business.Exceptions;
using OnlineShopProject.WebApi.Business.Services.Interface;
using OnlineShopProject.WebApi.Domain.Common.Paginations;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;
using OnlineShopProject.WebApi.EndPoint.Dto.RequestDto;
using OnlineShopProject.WebApi.EndPoint.Dto.ResponseDto;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;
using System.Security.Claims;

namespace OnlineShopProject.WebApi.Business.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly ICurrentUser _currentUser;

        public UserService(IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<bool> AddClaimToUserAsync(AddClaimToUserCommand addClaimToUserCommand)
        {
            await ChekingPermissionAsync(addClaimToUserCommand.RequesterId);

            var foundedUser = await _unitOfWork.UserManager.FindByIdAsync(addClaimToUserCommand.UserId.ToString());

            if (foundedUser is null)
                throw new NotFoundException("user not found");

            var userClaims = await _unitOfWork.UserManager.GetClaimsAsync(foundedUser);

            var existClaim = userClaims.Any(uc => uc.Type == addClaimToUserCommand.ClaimType && uc.Value == addClaimToUserCommand.ClaimValue);

            if (existClaim)
                throw new ConflictException("the claim is exist!!");

            var claim = new Claim(addClaimToUserCommand.ClaimType, addClaimToUserCommand.ClaimValue);

            var result = await _unitOfWork.UserManager.AddClaimAsync(foundedUser, claim);

            return result == IdentityResult.Success;
        }

        public async Task<bool> BanUserAsync(Guid userId, Guid requesterId)
        {
            var foundedUser = await AssignOwnerForBanningAndUnbanningAsync(userId, requesterId);

            if (foundedUser.Status == Domain.Entities.UserEntity.Enums.BanStatus.Banned)
                throw new InvalidOperationException("برادر چی میزنی ؟؟ به مام بده جنست خوبه");

            foundedUser.Banning(requesterId);

            await _unitOfWork.UserManager.UpdateAsync(foundedUser);

            return true;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordCommand changePasswordCommand)
        {
            var foundedUser = await _unitOfWork.UserManager.FindByIdAsync(changePasswordCommand.UserId);

            if (foundedUser is null)
                throw new NotFoundException("the user Not Found");

            var result = await _unitOfWork.UserManager.ChangePasswordAsync(foundedUser,changePasswordCommand.CurrentPassword,changePasswordCommand.newPassword);

            return result == IdentityResult.Success;
        }

        public async Task<bool> DeleteClaimFromUserAsync(DeleteClaimFromUserCommand deleteClaimFromUserCommand)
        {
            await ChekingPermissionAsync(deleteClaimFromUserCommand.RequesterId);

            var foundedUser = await _unitOfWork.UserManager.FindByIdAsync(deleteClaimFromUserCommand.UserId.ToString());

            if (foundedUser is null)
                throw new NotFoundException("user not found");

            var userClaims = await _unitOfWork.UserManager.GetClaimsAsync(foundedUser);

            var claim = userClaims.FirstOrDefault(uc => uc.Type == deleteClaimFromUserCommand.claimType && uc.Value == deleteClaimFromUserCommand.claimValue);

            if (claim is null)
                throw new ConflictException("the claim not exist!!");

            var result = await _unitOfWork.UserManager.RemoveClaimAsync(foundedUser, claim);

            return result == IdentityResult.Success;
        }

        public async Task<Pagination<UserClaimResponseDto>> GetUserClaimsAsync(Guid userId, PageRequest pageRequest)
        {
            var foundedUser = await _unitOfWork.UserManager.FindByIdAsync(userId.ToString());

            if (foundedUser is null)
                throw new NotFoundException("user not found");

            var userClaims = await _unitOfWork.UserManager.GetClaimsAsync(foundedUser);

            var result = userClaims
                .Select(uc => new UserClaimResponseDto(foundedUser.Id, uc.Type, uc.Value))
                .ToList();

            return Pagination<UserClaimResponseDto>.GetPagination(
                  result
                , pageRequest.PageNumber
                , pageRequest.PageSize
                , userClaims.Count());
        }

        public async Task<UserInfoResponseDto> GetUserInfoAsync(Guid userId)
        {
            var foundedUser = await _unitOfWork.UserManager.FindByIdAsync(userId.ToString());

            if (foundedUser is null)
                throw new NotFoundException("user not found");

            return new UserInfoResponseDto(foundedUser.FullName, foundedUser.Email!, foundedUser.PhoneNumber!);
        }

        public async Task<Pagination<UserRoleResponseDto>> GetUserRolesAsync(Guid userId, PageRequest pageRequest)
        {
            var foundedUser = await _unitOfWork.UserManager.FindByIdAsync(userId.ToString());

            if (foundedUser is null)
                throw new NotFoundException("user not found");

            var userRoles = await _unitOfWork.UserManager.GetRolesAsync(foundedUser);

            var result = userRoles
                .Select(ur => new UserRoleResponseDto(foundedUser.Id, ur))
                .ToList();

            return Pagination<UserRoleResponseDto>.GetPagination(
                  result
                , pageRequest.PageNumber
                , pageRequest.PageSize
                , userRoles.Count());
        }

        public async Task ToPerimum(Guid userId,Guid requesterId)
        {
          var user=  await _unitOfWork.UserManager.FindByIdAsync(userId.ToString());

            if(user is null)
                throw new NotFoundException($"{nameof(user)}.");

            user.ToProPlan();

            await _unitOfWork.UserManager.AddClaimAsync(user,ClaimConstants.VipFeature);

            await _unitOfWork.UserManager.UpdateAsync(user);
        }

        public async Task<bool> UnBanUserAsync(Guid userId, Guid requesterId)
        {
            var foundedUser = await AssignOwnerForBanningAndUnbanningAsync(userId, requesterId);

            if (foundedUser.Status != Domain.Entities.UserEntity.Enums.BanStatus.Banned)
                throw new InvalidOperationException("برادر چی میزنی ؟؟ به مام بده جنست خوبه");

            foundedUser.UnBanning(requesterId);

            await _unitOfWork.UserManager.UpdateAsync(foundedUser);

            return true;
        }

        private async Task<User> AssignOwnerForBanningAndUnbanningAsync(Guid userId, Guid requesterId)
        {
            var requester = await _unitOfWork.UserManager.FindByIdAsync(requesterId.ToString());

            if (requester == null)
                throw new NotFoundException("شما دسترسی نداری !!!!!!!");

            var requesterRoles = await _unitOfWork.UserManager.GetRolesAsync(requester);

            if (userId == requester.Id && !requesterRoles.Contains(RoleConstants.AdminRoleName))
                throw new PermissionDeniedException("شما دسترسی نداری!!!!!!!!!!");

            var user = await _unitOfWork.UserManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new NotFoundException("user not found for bann");

            return user;
        }

        private async Task ChekingPermissionAsync(Guid requesterId)
        {
            var requester = await _unitOfWork.UserManager.FindByIdAsync(requesterId.ToString());

            if (requester == null)
                throw new PermissionDeniedException("the requester not found");

            var requesterRoles = await _unitOfWork.UserManager.GetRolesAsync(requester);

            if (!requesterRoles.Contains(RoleConstants.AdminRoleName))
                throw new PermissionDeniedException("you must be the admin in the system");
        }


    }
}
