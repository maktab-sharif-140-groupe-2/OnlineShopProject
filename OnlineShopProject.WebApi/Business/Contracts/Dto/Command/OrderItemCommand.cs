namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public class OrderItemCommand
{
    public Guid ProductId { get; set; }
    public int Quantity { get;  set; }
    public Guid orderId { get; set; }

}
