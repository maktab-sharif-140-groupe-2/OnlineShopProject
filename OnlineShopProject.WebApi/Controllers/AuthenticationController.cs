using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.WebApi.Contracts.Dto.RequestDto;
using OnlineShopProject.WebApi.ServiceInterfaces;

namespace OnlineShopProject.WebApi.Controllers
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
        public async Task<IActionResult> RegisterUserAsync(RegisterRequestDto registerRequest)
        {
            var createdUserId = await _authenticationService.RegisterAsync(registerRequest);

            return Ok(registerRequest);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUserAsync(LoginRequestDto LoginRequest)
        {
            var userToken = await _authenticationService.TokenLoginAsync(LoginRequest);

            return Ok(userToken);
        }
    }
}
