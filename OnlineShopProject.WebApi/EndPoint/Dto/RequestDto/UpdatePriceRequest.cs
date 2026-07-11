namespace OnlineShopProject.WebApi.EndPoint.Dto.RequestDto
{
    public class UpdatePriceRequest
    {
        public Guid ProductId {  get; set; }
        public decimal NewPrice {  get; set; }
        public Guid ModifiederId {  get; set; }
    }
}
