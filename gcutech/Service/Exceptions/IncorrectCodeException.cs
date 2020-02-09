using System;
using System.Runtime.Serialization;

namespace gcutech.Service.Exceptions
{
    [Serializable]
    public class IncorrectCodeException : Exception
    {
        public IncorrectCodeException()
        {
        }

        public IncorrectCodeException(string message) : base(message)
        {
        }

        public IncorrectCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IncorrectCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}