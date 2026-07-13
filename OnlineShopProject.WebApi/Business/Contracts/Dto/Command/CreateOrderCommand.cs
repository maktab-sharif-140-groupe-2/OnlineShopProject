namespace OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

public class CreateOrderCommand
{
    public Guid ProductId { get; set; }
    public int Quantity { get;  set; }
    public Guid UserId { get; set; }

}
