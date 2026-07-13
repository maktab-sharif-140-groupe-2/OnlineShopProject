using OnlineShopProject.WebApi.Business.Contracts.Dto.Command;

namespace OnlineShopProject.WebApi.EndPoint.Dto.RequestDto
{
    public class CreateOrderRequestDto
    {
        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public CreateOrderCommand ToRequestDto()
        {
            return new CreateOrderCommand
            {
                ProductId = ProductId,
                Quantity = Quantity,
                UserId = UserId
            };
        }
    }
}
