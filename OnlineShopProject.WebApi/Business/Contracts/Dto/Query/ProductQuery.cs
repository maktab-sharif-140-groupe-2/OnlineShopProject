namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Query;

public class ProductQuery
{
    public string Name { get; set; }= string.Empty;
    public string BrandName { get; set; }=string.Empty;
    public decimal Price { get; set; }

}
