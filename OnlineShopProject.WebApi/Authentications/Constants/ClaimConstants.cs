using System.Security.Claims;

namespace OnlineShopProject.WebApi.Authentications.Constants;

public static class ClaimConstants
{
    public static readonly Claim ReadProduct = new("Product", "Read");
    public static readonly Claim ChangeProduct = new("Product", "Update");
    public static readonly Claim CreateProduct = new("Product", "Create");
    public static readonly Claim DeleteProduct = new("Product", "Delete");
    public static readonly Claim ReadOrder = new("Order", "Read");
    public static readonly Claim CreateOrder = new("Order", "Create");
    public static readonly Claim VipFeature = new("Premium", "Feature");
}
