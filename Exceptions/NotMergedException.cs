using System;
using System.Runtime.Serialization;

namespace FileComparator
{
    public class NotMergedException : Exception
    {
        private new const string Message = "Not all decisions where made";

        public NotMergedException()
        {
        }

        public NotMergedException(string message) : base(Message)
        {
        }

        public NotMergedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotMergedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
