using System;
using System.Runtime.Serialization;

namespace gcutech.Service.Exceptions
{
    [Serializable]
    public class CodeAlreadyMadeException : Exception
    {
        public CodeAlreadyMadeException()
        {
        }

        public CodeAlreadyMadeException(string message) : base(message)
        {
        }

        public CodeAlreadyMadeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CodeAlreadyMadeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}