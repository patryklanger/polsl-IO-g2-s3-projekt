using System;
using System.Runtime.Serialization;

namespace FileComparator
{
    public class UnresolvedConflictException : Exception
    {
        private new const string Message = "You must resolve conflict before making another decision!";

        public UnresolvedConflictException()
        {
        }

        public UnresolvedConflictException(string message) : base(Message)
        {
        }

        public UnresolvedConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnresolvedConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
