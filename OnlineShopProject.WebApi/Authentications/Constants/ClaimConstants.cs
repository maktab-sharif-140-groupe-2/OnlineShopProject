using System.Security.Claims;

namespace OnlineShopProject.WebApi.Authentications.Constants;

public static class ClaimConstants
{
    public static readonly Claim ReadProducts = new("Product", "Read");
    public static readonly Claim CreateOrder = new("Order", "Create");

    public static readonly Claim ChangeProduct = new("Product", "Update");
    public static readonly Claim CreateProduct = new("Product", "Create");
    public static readonly Claim DeleteProduct = new("Product", "Delete");
    public static readonly Claim ReadOrders = new("Order", "Read");

    public static readonly Claim VipFeature = new("Premium", "Feature");
}
