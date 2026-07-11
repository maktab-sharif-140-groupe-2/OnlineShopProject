using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Domain.Common.Paginations;
using OnlineShopProject.WebApi.EndPoint.Dto.RequestDto;
using OnlineShopProject.WebApi.EndPoint.Dto.ResponseDto;

namespace OnlineShopProject.WebApi.Business.Services.Interface
{
    public interface IUserService
    {
        Task<UserInfoResponseDto> GetUserInfoAsync(Guid userId);

        Task<Pagination<UserClaimResponseDto>> GetUserClaimsAsync(Guid userId, PageRequest pageRequest);

        Task<Pagination<UserRoleResponseDto>> GetUserRolesAsync(Guid userId, PageRequest pageRequest);

        Task<bool> BanUserAsync(Guid userId, Guid requesterId);

        Task<bool> UnBanUserAsync(Guid userId, Guid requesterId);

        Task<bool> DeleteClaimFromUserAsync(DeleteClaimFromUserCommand deleteClaimFromUserCommand);

        Task<bool> AddClaimToUserAsync(AddClaimToUserCommand addClaimToUserCommand);

        Task<bool> ChangePasswordAsync(ChangePasswordCommand changePasswordCommand);
    }
}
