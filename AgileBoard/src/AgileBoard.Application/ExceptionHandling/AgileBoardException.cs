using System.Globalization;

namespace AgileBoard.Application.ExceptionHandling
{
    public class AgileBoardException : Exception
    {
        public AgileBoardException() : base() { }

        public AgileBoardException(string message) : base(message) { }

        public AgileBoardException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
