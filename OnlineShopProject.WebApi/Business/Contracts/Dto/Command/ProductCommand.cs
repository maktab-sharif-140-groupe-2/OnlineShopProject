namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public class ProductCommand
{
    public string Name { get; set; } = string.Empty;
    public string BrandName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }

}
