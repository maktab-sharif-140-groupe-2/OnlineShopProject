using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Business.Services.Interface;

namespace OnlineShopProject.WebApi.EndPoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync(RegisterCommand registerRequest)
        {
            var createdUserId = await _authenticationService.RegisterAsync(registerRequest);

            return Ok(registerRequest);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAsync(LoginCommand LoginRequest)
        {
            var userToken = await _authenticationService.TokenLoginAsync(LoginRequest);

            return Ok(userToken);
        }
    }
}
