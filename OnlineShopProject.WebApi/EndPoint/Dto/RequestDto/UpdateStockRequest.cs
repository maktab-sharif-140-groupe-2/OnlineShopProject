namespace OnlineShopProject.WebApi.EndPoint.Dto.RequestDto
{
    public class UpdateStockRequest
    {
        public Guid productId { get; set; }
        public int stock { get; set; }
        public Guid modifierId { get; set; }
    }
}
