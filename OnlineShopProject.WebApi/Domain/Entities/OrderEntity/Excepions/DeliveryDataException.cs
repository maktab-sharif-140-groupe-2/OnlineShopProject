namespace OnlineShopProject.WebApi.Domain.Entities.OrderEntity.Excepions
{
    [Serializable]
    internal class DeliveryDataException : Exception
    {
        public DeliveryDataException()
        {
        }

        public DeliveryDataException(string? message) : base(message)
        {
        }

        public DeliveryDataException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}