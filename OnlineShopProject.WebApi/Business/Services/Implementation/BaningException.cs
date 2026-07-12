namespace OnlineShopProject.WebApi.Business.Services.Implementation
{
    [Serializable]
    internal class BaningException : Exception
    {
        public BaningException()
        {
        }

        public BaningException(string? message) : base(message)
        {
        }

        public BaningException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}