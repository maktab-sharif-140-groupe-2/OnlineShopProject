using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShopProject.WebApi.Authentications.Constants;
using OnlineShopProject.WebApi.Contracts.Dto.RequestDto;
using OnlineShopProject.WebApi.Contracts.Dto.ResponseDto;
using OnlineShopProject.WebApi.Contracts.JwtSetting;
using OnlineShopProject.WebApi.Entities.RoleEntity;
using OnlineShopProject.WebApi.Entities.UserEntity;
using OnlineShopProject.WebApi.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace OnlineShopProject.WebApi.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(UserManager<User> userManager, RoleManager<Role> roleManager, SignInManager<User> signInManager, IOptions<JwtSettings> options)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _jwtSettings = options.Value;
    }

    public async Task<Guid> RegisterAsync(RegisterRequestDto registerRequest)
    {
        var duplicateUserName = await _userManager.FindByEmailAsync(registerRequest.Email);

        if (duplicateUserName is not null)
            throw new InvalidOperationException("the user with this email is already exist");

        var user = new User(registerRequest.FullName, registerRequest.Age, registerRequest.Email, registerRequest.PhoneNumber);

        var createUserResult = await _userManager.CreateAsync(user, registerRequest.Password);

        if (!createUserResult.Succeeded)
            throw new InvalidOperationException(createUserResult.Errors.FirstOrDefault()?.Description ?? "Registration failed.");

        var addRoleResult = await _userManager.AddToRoleAsync(user, RoleConstants.UserRoleName);

        if (!addRoleResult.Succeeded)
            throw new InvalidOperationException(createUserResult.Errors.FirstOrDefault()?.Description ?? "Registration failed.");

        return user.Id;
    }

    public async Task<TokenLoginResponseDto> TokenLoginAsync(LoginRequestDto loginRequest)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, true);

        if (signInResult.IsLockedOut)
            throw new InvalidOperationException("shoma karbar ahianan framoshi dari enghad eshtbah mizani!!bi adab");

        if (signInResult.IsNotAllowed)
            throw new InvalidOperationException("har ki bra khodesh nis ke boro bozorg shodi bia");

        if (!signInResult.Succeeded)
            throw new InvalidOperationException("bi adab password ya user name eshtbahe");

        var foundedUser = await _userManager.FindByNameAsync(loginRequest.UserName);

        if (foundedUser is null)
            throw new InvalidOperationException("peida nashodi azizam");

        return await GenerateTokenAsync(foundedUser);
    }

    private async Task<TokenLoginResponseDto> GenerateTokenAsync(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!),
        };

        var userRoles = await _userManager.GetRolesAsync(user);

        var userRoleClaims = userRoles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

        foreach (var roleClaim in userRoleClaims)
        {
            var role = _roleManager.Roles.FirstOrDefault(r => r.Name == roleClaim.Value!);

            var roleClaims = await _roleManager.GetClaimsAsync(role);

            claims.AddRange(roleClaims);
        }

        claims.AddRange(userRoleClaims);

        var userClaims = await _userManager.GetClaimsAsync(user);

        claims.AddRange(userClaims);

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

        var encryptKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.EncryptKey));

        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var encryptCredential = new EncryptingCredentials(encryptKey, SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

        var expiresIn = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes);

        //var token = new JwtSecurityToken(
        //    _jwtSettings.Issuer,
        //    _jwtSettings.Audience,
        //    claims,
        //    expires: expiresIn,
        //    signingCredentials: credentials);

        //امنیتی 100
        var descriptor = new SecurityTokenDescriptor()
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Subject = new ClaimsIdentity(claims),
            Expires = expiresIn,
            SigningCredentials = credentials,
            EncryptingCredentials = encryptCredential,
        };

        var token = new JwtSecurityTokenHandler().CreateToken(descriptor);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token)!;
        var expiresInSeconds = expiresIn.Subtract(DateTime.UtcNow).TotalSeconds;
        return new TokenLoginResponseDto(accessToken, expiresInSeconds);
    }
}
