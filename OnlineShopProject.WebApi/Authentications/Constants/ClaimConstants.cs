using System.Security.Claims;

namespace OnlineShopProject.WebApi.Authentications.Constants;

public static class ClaimConstants
{
    public static readonly Claim ProUser = new("Plan", "Pro");
    public static readonly Claim FreeUser = new("Plan", "Free");
    public static readonly Claim ActiveStatus = new("Status", "Active");
    public static readonly Claim BannedStatus = new("Status", "Banned");
}
