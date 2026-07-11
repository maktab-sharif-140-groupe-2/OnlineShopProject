using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineShopProject.WebApi.Authentications.Constants;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;
using OnlineShopProject.WebApi.Business.Contracts.Dto.Query;
using OnlineShopProject.WebApi.Business.Contracts.JwtSetting;
using OnlineShopProject.WebApi.Business.Services.Interface;
using OnlineShopProject.WebApi.Domain.Entities.UserEntity;
using OnlineShopProject.WebApi.Infrastructure.Repositories.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineShopProject.WebApi.Business.Services.Implementation;

public class AuthenticationService : IAuthenticationService
{
    private readonly JwtSettings _jwtSettings;

    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationService(IUnitOfWork unitOfWork, IOptions<JwtSettings> options)
    {
        _unitOfWork = unitOfWork;
        _jwtSettings = options.Value;
    }

    public async Task<Guid> RegisterAsync(RegisterCommand registerRequest)
    {
        var duplicateUserName = await _unitOfWork.UserManager.FindByEmailAsync(registerRequest.Email);

        if (duplicateUserName is not null)
            throw new InvalidOperationException("the user with this email is already exist");

        var user = new User(registerRequest.FullName, registerRequest.Age, registerRequest.Email, registerRequest.PhoneNumber);

        var createUserResult = await _unitOfWork.UserManager.CreateAsync(user, registerRequest.Password);

        if (!createUserResult.Succeeded)
            throw new InvalidOperationException(createUserResult.Errors.FirstOrDefault()?.Description ?? "Registration failed.");

        var addRoleResult = await _unitOfWork.UserManager.AddToRoleAsync(user, RoleConstants.UserRoleName);

        if (!addRoleResult.Succeeded)
            throw new InvalidOperationException(createUserResult.Errors.FirstOrDefault()?.Description ?? "Registration failed.");

        return user.Id;
    }

    public async Task<TokenLoginQuery> TokenLoginAsync(LoginCommand loginRequest)
    {
        var signInResult = await _unitOfWork._signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, true);

        if (signInResult.IsLockedOut)
            throw new InvalidOperationException("shoma karbar ahianan framoshi dari enghad eshtbah mizani!!bi adab");

        if (signInResult.IsNotAllowed)
            throw new InvalidOperationException("har ki bra khodesh nis ke boro bozorg shodi bia");

        if (!signInResult.Succeeded)
            throw new InvalidOperationException("bi adab password ya user name eshtbahe");

        var foundedUser = await _unitOfWork.UserManager.FindByNameAsync(loginRequest.UserName);

        if (foundedUser is null)
            throw new InvalidOperationException("peida nashodi azizam");

        return await GenerateTokenAsync(foundedUser);
    }

    private async Task<TokenLoginQuery> GenerateTokenAsync(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!),
        };

        var userRoles = await _unitOfWork.UserManager.GetRolesAsync(user);

        var userRoleClaims = userRoles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

        foreach (var roleClaim in userRoleClaims)
        {
            var role = _unitOfWork._roleManager.Roles.FirstOrDefault(r => r.Name == roleClaim.Value!);

            var roleClaims = await _unitOfWork._roleManager.GetClaimsAsync(role);

            claims.AddRange(roleClaims);
        }

        claims.AddRange(userRoleClaims);

        var userClaims = await _unitOfWork.UserManager.GetClaimsAsync(user);

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
        return new TokenLoginQuery(accessToken, expiresInSeconds);
    }
}
