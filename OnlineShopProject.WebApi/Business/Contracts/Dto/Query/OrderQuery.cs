namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Query;

public class OrderQuery
{
    public List<ProductQuery> Items { get; set; } = [];
    public string OrdererName { get; set; }=string.Empty;
    public decimal TotalPrice { get; set; }
    public DateOnly? DeliveryDate { get; set; }
    public string Status { get; set; } = string.Empty;

}
