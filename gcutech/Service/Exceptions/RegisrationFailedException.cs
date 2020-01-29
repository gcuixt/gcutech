using System;
using System.Runtime.Serialization;

namespace gcutech.Service.Exceptions
{
    [Serializable]
    public class RegisrationFailedException : Exception
    {
        public RegisrationFailedException()
        {
        }

        public RegisrationFailedException(string message) : base(message)
        {
        }

        public RegisrationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegisrationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}