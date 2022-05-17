using System.Globalization;

namespace TaskTracker.Models.Exceptions
{
    public class ServerException : Exception
    {
        public ServerException()
        {
        }

        public ServerException(string message) : base(message) { }

        public ServerException(string message, Exception innerException) : base(message, innerException) { }

        public ServerException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
