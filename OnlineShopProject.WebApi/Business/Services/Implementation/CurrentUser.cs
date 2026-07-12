using OnlineShopProject.WebApi.Business.Services.Interface;
using System.Security.Claims;

namespace OnlineShopProject.WebApi.Business.Services.Implementation;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid? UserId => GenerateId(_contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

    private Guid? GenerateId(string? value)
    {
       var result= Guid.TryParse(value, out var id);

        if(result)
            return id;

        return null;
        

    }
    
}
