using System;
using System.Runtime.Serialization;

namespace DannyBoyNg.Services
{
    [Serializable]
    public class MailServerNotSetException : Exception
    {
        public MailServerNotSetException()
        {
        }

        public MailServerNotSetException(string message) : base(message)
        {
        }

        public MailServerNotSetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MailServerNotSetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}