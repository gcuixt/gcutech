using System;
using System.Runtime.Serialization;

namespace gcutech.Service.Exceptions
{
    [Serializable]
    public class DeleteFailedException : Exception
    {
        public DeleteFailedException()
        {
        }

        public DeleteFailedException(string message) : base(message)
        {
        }

        public DeleteFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DeleteFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}