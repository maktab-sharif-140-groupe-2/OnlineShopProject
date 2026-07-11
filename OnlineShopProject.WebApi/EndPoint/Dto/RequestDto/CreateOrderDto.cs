namespace OnlineShopProject.WebApi.EndPoint.Dto.RequestDto
{
    public class CreateOrderDto
    {
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

      
    }
    public class OrderDto
    {
        public Guid userId { get; set; }
        public Guid? createrId { get; set; } = null;
    }
}
