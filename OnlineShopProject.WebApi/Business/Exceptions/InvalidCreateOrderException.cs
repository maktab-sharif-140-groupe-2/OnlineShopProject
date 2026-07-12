namespace OnlineShopProject.WebApi.Business.Exceptions
{
    [Serializable]
    internal class InvalidCreateOrderException : Exception
    {
        public InvalidCreateOrderException()
        {
        }

        public InvalidCreateOrderException(string? message) : base(message)
        {
        }

        public InvalidCreateOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}