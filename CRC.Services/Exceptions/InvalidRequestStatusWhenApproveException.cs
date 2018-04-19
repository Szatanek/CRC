using System;

namespace CRC.Services.Exceptions
{
    public sealed class InvalidRequestStatusWhenApproveException : Exception
    {
        private const string DomainMessage = "Request can be approved only in In Progress status.";

        public InvalidRequestStatusWhenApproveException()
            : base(DomainMessage)
        {
        }
    }
}