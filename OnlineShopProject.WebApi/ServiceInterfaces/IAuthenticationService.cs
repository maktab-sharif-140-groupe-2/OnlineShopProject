using OnlineShopProject.WebApi.Contracts.Dto.RequestDto;
using OnlineShopProject.WebApi.Contracts.Dto.ResponseDto;

namespace OnlineShopProject.WebApi.ServiceInterfaces;

public interface IAuthenticationService
{
    Task<Guid> RegisterAsync(RegisterRequestDto registerRequest);

    Task<TokenLoginResponseDto> TokenLoginAsync(LoginRequestDto loginCommand);
}
