using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Query;

namespace OnlineShopProject.WebApi.Business.Services.Interface;

public interface IAuthenticationService
{
    Task<Guid> RegisterAsync(RegisterCommand registerRequest);

    Task<TokenLoginQuery> TokenLoginAsync(LoginCommand loginCommand);
}
