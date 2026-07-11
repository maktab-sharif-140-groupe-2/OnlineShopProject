namespace OnlineShopProject.WebApi.Business.Exceptions
{
    [Serializable]
    internal class DouplicateException : Exception
    {
        public DouplicateException()
        {
        }

        public DouplicateException(string? message) : base(message)
        {
        }

        public DouplicateException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}