namespace OnlineShopProject.WebApi.Domain.Entities.ProductEntity.Excepitons
{
    [Serializable]
    internal class ProcessingStockException : Exception
    {
        public ProcessingStockException()
        {
        }

        public ProcessingStockException(string? message) : base(message)
        {
        }

        public ProcessingStockException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}