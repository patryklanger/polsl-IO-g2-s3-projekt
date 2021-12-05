using System;
using System.Runtime.Serialization;

namespace FileComparator
{
    public class NotComparedException : Exception
    {
        private new const string Message = "Texts not compared";

        public NotComparedException()
        {
        }

        public NotComparedException(string message) : base(Message)
        {
        }

        public NotComparedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotComparedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}