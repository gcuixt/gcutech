using System;
using System.Runtime.Serialization;

namespace gcutech.Service.Exceptions
{
    [Serializable]
    public class AlreadyCheckedInException : Exception
    {
        public AlreadyCheckedInException()
        {
        }

        public AlreadyCheckedInException(string message) : base(message)
        {
        }

        public AlreadyCheckedInException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyCheckedInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}