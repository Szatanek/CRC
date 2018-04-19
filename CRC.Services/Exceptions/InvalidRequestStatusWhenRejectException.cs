using System;

namespace CRC.Services.Exceptions
{
    public sealed class InvalidRequestStatusWhenRejectException : Exception
    {
        private const string DomainMessage = "Request can be rejected only in In Progress status.";

        public InvalidRequestStatusWhenRejectException()
            : base(DomainMessage)
        {
        }
    }
}