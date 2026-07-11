namespace OnlineShopProject.WebApi.Domain.Entities.ProductEntity.Excepitons
{
    [Serializable]
    internal class InputPriceException : Exception
    {
        public InputPriceException()
        {
        }

        public InputPriceException(string? message) : base(message)
        {
        }

        public InputPriceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}