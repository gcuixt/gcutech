using System;
using System.Runtime.Serialization;

namespace gcutech.Service.Exceptions
{
    [Serializable]
    public class AccountRegisrationFailedException : Exception
    {
        public AccountRegisrationFailedException()
        {
        }

        public AccountRegisrationFailedException(string message) : base(message)
        {
        }

        public AccountRegisrationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountRegisrationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}